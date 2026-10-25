using iTextSharp.text;
using iTextSharp.text.pdf;
using Prj_Capa_Negocio;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using PdfiumViewer;

// Alias para evitar conflictos con Crystal Reports
using PdfParagraph = iTextSharp.text.Paragraph;
using PdfFontFactory = iTextSharp.text.FontFactory;
using PdfBaseColor = iTextSharp.text.BaseColor;
using PdfElement = iTextSharp.text.Element;

namespace MicroSisPlani.Informes
{
    public partial class Frm_PrintAsis_deldia : Form
    {
        private PdfViewer pdfViewer;

        public Frm_PrintAsis_deldia()
        {
            InitializeComponent();

            // Crear visor PDF embebido
            pdfViewer = new PdfViewer();
            pdfViewer.Dock = DockStyle.Fill;
            this.Controls.Add(pdfViewer);
        }

        public string tipoInfo = "";

        private void Frm_PrintAsis_deldia_Load(object sender, EventArgs e)
        {
            if (tipoInfo == "deldia")
            {
                Generar_InformedelDia();
            }
            else if (tipoInfo == "delmes")
            {
                Generar_InformedelMes();
            }
        }

        private void Generar_InformedelDia()
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable data = obj.RN_Ver_Todas_Asistencias_Deldia(Convert.ToDateTime(this.Tag));

            GenerarInformePDF(data, "InformeDelDia.pdf", "Informe de Asistencias del Día");
        }

        private void Generar_InformedelMes()
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable data = obj.RN_Ver_Todas_Asistencias_DelMes(Convert.ToDateTime(this.Tag));

            GenerarInformePDF(data, "InformeDelMes.pdf", "Informe de Asistencias del Mes");
        }

        private void GenerarInformePDF(DataTable data, string nombreArchivo, string tituloTexto)
        {
            if (data.Rows.Count > 0)
            {
                // Tu resumen intacto 💖
                DataTable resumen = new DataTable();
                resumen.Columns.Add("Nombre Completo");
                resumen.Columns.Add("ID Asistencia");
                resumen.Columns.Add("Fecha Asistencia");
                resumen.Columns.Add("Hora Entrada");
                resumen.Columns.Add("Hora Salida");
                resumen.Columns.Add("Horas Trabajadas");

                foreach (DataRow row in data.Rows)
                {
                    resumen.Rows.Add(
                        row["Nombre_Completo"],
                        row["Id_asis"],
                        row["FechaAsis"],
                        row["HoIngreso"],
                        row["HoSalida"],
                        row["Total_HrsWorked"]
                    );
                }

                string ruta = Path.Combine(Application.StartupPath, nombreArchivo);

                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter.GetInstance(doc, fs);

                    doc.Open();

                    PdfParagraph titulo = new PdfParagraph(
                        tituloTexto,
                        PdfFontFactory.GetFont(PdfFontFactory.HELVETICA_BOLD, 16, PdfBaseColor.PINK)
                    );
                    titulo.Alignment = PdfElement.ALIGN_CENTER;
                    doc.Add(titulo);
                    doc.Add(new PdfParagraph("\n"));

                    PdfPTable table = new PdfPTable(resumen.Columns.Count);
                    table.WidthPercentage = 100;
                    table.SpacingBefore = 20f;

                    foreach (DataColumn col in resumen.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(col.ColumnName,
                            PdfFontFactory.GetFont(PdfFontFactory.HELVETICA_BOLD, 12, PdfBaseColor.WHITE)));
                        cell.BackgroundColor = new PdfBaseColor(255, 182, 193);
                        cell.HorizontalAlignment = PdfElement.ALIGN_CENTER;
                        table.AddCell(cell);
                    }

                    int rowIndex = 0;
                    foreach (DataRow row in resumen.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(item.ToString(),
                                PdfFontFactory.GetFont(PdfFontFactory.HELVETICA, 11)));
                            cell.HorizontalAlignment = PdfElement.ALIGN_CENTER;

                            if (rowIndex % 2 == 0)
                                cell.BackgroundColor = new PdfBaseColor(255, 240, 245);

                            table.AddCell(cell);
                        }
                        rowIndex++;
                    }

                    doc.Add(table);
                    doc.Close();
                }

                // Ahora sí cargar el PDF en el visor
                pdfViewer1.Document = PdfiumViewer.PdfDocument.Load(ruta);
            }
            else
            {
                MessageBox.Show("No hay asistencias registradas.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            if (pdfViewer1.Document != null)
            {
                pdfViewer1.Document.Dispose();
                pdfViewer1.Document = null;
            }

            this.Close();
        }

    }
}