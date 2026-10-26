using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MicroSisPlani.Informes
{
    /// <summary>
    /// Genera en PDF (iTextSharp, igual que los informes de asistencia) el reporte semanal
    /// de nómina y los recibos por empleado. Los guarda en Documentos\Nomina_La_Grandeza.
    /// </summary>
    public static class NominaPdf
    {
        private static readonly CultureInfo Mx = new CultureInfo("es-MX");
        private static readonly BaseColor Azul = new BaseColor(0, 51, 102);
        private static readonly BaseColor Gris = new BaseColor(235, 235, 235);

        // ------------------------------------------------------------
        // REPORTE GENERAL (una fila por empleado, con columna de firma)
        // ------------------------------------------------------------
        public static string GenerarReporte(DataTable dt, DateTime lunes, DateTime domingo, string sucursal)
        {
            string ruta = Path.Combine(Carpeta(),
                "Nomina_" + lunes.ToString("yyyyMMdd") + "_" + domingo.ToString("yyyyMMdd") + ".pdf");

            using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document(PageSize.A4.Rotate(), 30, 30, 30, 30);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                Encabezado(doc, "NÓMINA SEMANAL", lunes, domingo, sucursal);

                // Sucursal, Empleado, Horas trab., Horas extra, Faltas, Susp., Tardanzas, Descuento, Total, Firma
                float[] anchos = { 15, 24, 9, 8, 7, 7, 9, 11, 12, 18 };
                PdfPTable t = new PdfPTable(anchos.Length);
                t.WidthPercentage = 100;
                t.SpacingBefore = 10f;
                t.SetWidths(anchos);

                string[] titulos = { "Sucursal", "Empleado", "Horas trab.", "Horas extra", "Faltas", "Susp.",
                                     "Tardanzas", "Descuento", "Total a pagar", "Firma" };
                foreach (string h in titulos)
                    t.AddCell(Celda(h, F(9, true, BaseColor.WHITE), Element.ALIGN_CENTER, Azul));

                decimal total = 0;
                int i = 0;
                foreach (DataRow r in dt.Rows)
                {
                    BaseColor fondo = (i % 2 == 0) ? null : Gris;
                    decimal bruto = D(r, "Sueldo_Bruto");
                    total += bruto;

                    t.AddCell(Celda(S(r, "Sucursal"), F(9), Element.ALIGN_LEFT, fondo));
                    t.AddCell(Celda(S(r, "Empleado"), F(9), Element.ALIGN_LEFT, fondo));
                    t.AddCell(Celda(D(r, "Horas_Trabajadas").ToString("N2", Mx), F(9), Element.ALIGN_RIGHT, fondo));
                    t.AddCell(Celda(D(r, "Horas_Extra").ToString("N2", Mx), F(9), Element.ALIGN_RIGHT, fondo));
                    t.AddCell(Celda(S(r, "Faltas"), F(9), Element.ALIGN_CENTER, fondo));
                    t.AddCell(Celda(S(r, "Suspensiones"), F(9), Element.ALIGN_CENTER, fondo));
                    t.AddCell(Celda(S(r, "Tardanzas"), F(9), Element.ALIGN_CENTER, fondo));
                    t.AddCell(Celda(D(r, "Descuento").ToString("C2", Mx), F(9), Element.ALIGN_RIGHT, fondo));
                    t.AddCell(Celda(bruto.ToString("C2", Mx), F(9, true), Element.ALIGN_RIGHT, fondo));
                    t.AddCell(Celda("", F(9), Element.ALIGN_LEFT, fondo));
                    i++;
                }

                PdfPCell lbl = Celda("TOTAL A PAGAR", F(10, true), Element.ALIGN_RIGHT, Gris);
                lbl.Colspan = 8;
                t.AddCell(lbl);
                t.AddCell(Celda(total.ToString("C2", Mx), F(10, true), Element.ALIGN_RIGHT, Gris));
                t.AddCell(Celda("", F(9), Element.ALIGN_LEFT, Gris));

                doc.Add(t);

                Paragraph nota = new Paragraph(
                    "Se paga por horas trabajadas. Faltas y suspensiones no se pagan; las horas extra se pagan al mismo " +
                    "pago por hora. Sin deducciones.", F(8));
                nota.SpacingBefore = 8f;
                doc.Add(nota);

                doc.Close();
            }
            return ruta;
        }

        // ------------------------------------------------------------
        // RECIBOS (2 por hoja), solo para empleados con sueldo capturado
        // ------------------------------------------------------------
        public static string GenerarRecibos(DataTable dt, DateTime lunes, DateTime domingo)
        {
            string ruta = Path.Combine(Carpeta(),
                "Recibos_" + lunes.ToString("yyyyMMdd") + "_" + domingo.ToString("yyyyMMdd") + ".pdf");

            using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document(PageSize.A4, 40, 40, 30, 30);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                int n = 0;
                foreach (DataRow r in dt.Rows)
                {
                    if (D(r, "Horas_Por_Trabajar") <= 0) continue;   // sin sueldo capturado: no hay recibo

                    if (n > 0 && n % 2 == 0) doc.NewPage();
                    doc.Add(Recibo(r, lunes, domingo));

                    Paragraph espacio = new Paragraph(" ");
                    espacio.SpacingAfter = 18f;
                    doc.Add(espacio);
                    n++;
                }

                if (n == 0)
                    doc.Add(new Paragraph("No hay empleados con sueldo capturado en este periodo.", F(11)));

                doc.Close();
            }
            return ruta;
        }

        private static PdfPTable Recibo(DataRow r, DateTime lunes, DateTime domingo)
        {
            PdfPTable t = new PdfPTable(2);
            t.WidthPercentage = 100;
            t.SetWidths(new float[] { 60, 40 });

            PdfPCell cab = Celda("LA GRANDEZA HD CREPERÍA  -  RECIBO DE NÓMINA SEMANAL",
                                 F(11, true, BaseColor.WHITE), Element.ALIGN_CENTER, Azul);
            cab.Colspan = 2;
            cab.Padding = 8f;
            t.AddCell(cab);

            Datos(t, "Empleado", S(r, "Empleado"));
            Datos(t, "Sucursal", S(r, "Sucursal"));
            Datos(t, "Horario", S(r, "Horario"));
            Datos(t, "Periodo", "Del " + lunes.ToString("dd/MM/yyyy") + " al " + domingo.ToString("dd/MM/yyyy"));

            decimal horasExtra = D(r, "Horas_Extra");
            decimal descuento = D(r, "Descuento");

            Fila(t, "Sueldo fijo semanal", D(r, "Sueldo_Fijo").ToString("C2", Mx), false);
            Fila(t, "Horas por trabajar", D(r, "Horas_Por_Trabajar").ToString("N2", Mx), false);
            Fila(t, "Pago por hora", D(r, "Pago_Por_Hora").ToString("C2", Mx), false);
            Fila(t, "Horas trabajadas", D(r, "Horas_Trabajadas").ToString("N2", Mx), false);
            Fila(t, "Horas extra (" + horasExtra.ToString("N2", Mx) + " h)",
                 "+ " + D(r, "Pago_Extra").ToString("C2", Mx), false);
            Fila(t, "Descuento por horas no trabajadas (faltas: " + S(r, "Faltas") +
                    ", suspensiones: " + S(r, "Suspensiones") + ")",
                 "- " + descuento.ToString("C2", Mx), false);

            PdfPCell l = Celda("TOTAL A PAGAR", F(12, true), Element.ALIGN_LEFT, Gris);
            PdfPCell v = Celda(D(r, "Sueldo_Bruto").ToString("C2", Mx), F(12, true), Element.ALIGN_RIGHT, Gris);
            l.Padding = 8f; v.Padding = 8f;
            t.AddCell(l);
            t.AddCell(v);

            PdfPCell firma = new PdfPCell(new Phrase("\n\n_______________________________\nFirma de recibido", F(9)));
            firma.Colspan = 2;
            firma.HorizontalAlignment = Element.ALIGN_CENTER;
            firma.Border = Rectangle.NO_BORDER;
            firma.PaddingTop = 10f;
            t.AddCell(firma);

            return t;
        }

        // ------------------------------------------------------------
        // AUXILIARES
        // ------------------------------------------------------------
        public static void Abrir(string ruta)
        {
            Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
        }

        private static string Carpeta()
        {
            string dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Nomina_La_Grandeza");
            Directory.CreateDirectory(dir);
            return dir;
        }

        private static void Encabezado(Document doc, string titulo, DateTime lunes, DateTime domingo, string sucursal)
        {
            Paragraph a = new Paragraph("LA GRANDEZA HD CREPERÍA", F(11, true, Azul));
            a.Alignment = Element.ALIGN_CENTER;
            doc.Add(a);

            Paragraph b = new Paragraph(titulo, F(16, true));
            b.Alignment = Element.ALIGN_CENTER;
            doc.Add(b);

            Paragraph c = new Paragraph(
                "Semana del " + lunes.ToString("dd/MM/yyyy") + " al " + domingo.ToString("dd/MM/yyyy") +
                "   |   Sucursal: " + sucursal, F(10));
            c.Alignment = Element.ALIGN_CENTER;
            doc.Add(c);
        }

        private static void Datos(PdfPTable t, string etiqueta, string valor)
        {
            PdfPCell a = Celda(etiqueta + ":", F(10, true), Element.ALIGN_LEFT, null);
            PdfPCell b = Celda(valor, F(10), Element.ALIGN_LEFT, null);
            a.Border = Rectangle.LEFT_BORDER;
            b.Border = Rectangle.RIGHT_BORDER;
            t.AddCell(a);
            t.AddCell(b);
        }

        private static void Fila(PdfPTable t, string concepto, string valor, bool negrita)
        {
            t.AddCell(Celda(concepto, F(10, negrita), Element.ALIGN_LEFT, null));
            t.AddCell(Celda(valor, F(10, negrita), Element.ALIGN_RIGHT, null));
        }

        private static PdfPCell Celda(string texto, Font fuente, int alineacion, BaseColor fondo)
        {
            PdfPCell c = new PdfPCell(new Phrase(texto, fuente));
            c.HorizontalAlignment = alineacion;
            c.VerticalAlignment = Element.ALIGN_MIDDLE;
            c.Padding = 5f;
            if (fondo != null) c.BackgroundColor = fondo;
            return c;
        }

        private static Font F(float tamano, bool negrita = false, BaseColor color = null)
        {
            return FontFactory.GetFont(
                negrita ? FontFactory.HELVETICA_BOLD : FontFactory.HELVETICA,
                tamano,
                color ?? BaseColor.BLACK);
        }

        private static string S(DataRow r, string col)
        {
            return Convert.ToString(r[col]).Trim();
        }

        private static decimal D(DataRow r, string col)
        {
            object v = r[col];
            return (v == null || v == DBNull.Value) ? 0m : Convert.ToDecimal(v);
        }
    }
}
