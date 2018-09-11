using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

namespace BOL.Helpers
{
    public static class Util
    {
        public static string DevuelveTablaHtml(DataTable origen)
        {

            StringBuilder TablaHtml = new StringBuilder();           
                     
            TablaHtml.AppendLine("<thead>");
            TablaHtml.AppendLine("<th>#</th>");
            foreach (DataColumn item in origen.Columns)
            {
                TablaHtml.AppendLine("<th>" + item.ColumnName + "</th>");                
            }
            TablaHtml.AppendLine("</thead>");
            TablaHtml.AppendLine("<tbody>");




            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");
                TablaHtml.AppendLine("<td><a href='javaScript:void(0)'><button type='button'>Asignar</button></a></td>");       
                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {
                    TablaHtml.AppendLine("<td>" +origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }

            TablaHtml.AppendLine("</tbody>");         
            return TablaHtml.ToString();

        }

        public static string DevuelveTablaMod(DataTable origen , string nombre )
        {

            StringBuilder TablaHtml = new StringBuilder();

            TablaHtml.AppendLine("<thead>");

            TablaHtml.AppendLine("<th>#</th>");
            foreach (DataColumn item in origen.Columns)
            {
                TablaHtml.AppendLine("<th>" + item.ColumnName + "</th>");
            }
            TablaHtml.AppendLine("</thead>");
            TablaHtml.AppendLine("<tbody>");

            

            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");
                TablaHtml.AppendLine("<td><a href='javaScript:void(0)'><button type='button'>"+ nombre + "</button></a></td>");
                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {
                    TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }

            TablaHtml.AppendLine("</tbody>");
            return TablaHtml.ToString();

        }


        public static string DevuelveBodyPersonas(DataTable origen)
        {

            StringBuilder TablaHtml = new StringBuilder();

            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");

                if (origen.Rows[cnt_filas]["Estado"].ToString() == "Aprobado")
                {                                     

                    TablaHtml.AppendLine("<td> <input  class='btn-success' type ='button' value ='Aprobado' ></td>");
                 

                }
                else
                {
                    TablaHtml.AppendLine("<td><button type='button' class='btn-danger btn_eliminar_det'>Quitar</button></td>");
                }

                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {
                    TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }

            return TablaHtml.ToString();
        }


        public static string DevuelveBodyHtml(DataTable origen)
        {

            StringBuilder TablaHtml = new StringBuilder();                                 
                                
            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");                                                             
               
               TablaHtml.AppendLine("<td><button type='button' class='btn-danger btn_eliminar_det'>Quitar</button></td>");             
    
                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {
                    TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }
          
            return TablaHtml.ToString();
        }



        public static string DevuelveBodyHtmlNormalHidden(DataTable origen , int col)
        {

            StringBuilder TablaHtml = new StringBuilder();
            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");
                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {
                    if (cnt_columnas != col)
                    {
                        TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                    }
                }

                 
                TablaHtml.AppendLine("</tr>");
            }
            return TablaHtml.ToString();
        }



        public static string DevuelveBodyHtmlNormal(DataTable origen)
        {

            StringBuilder TablaHtml = new StringBuilder();
            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");                                
                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {
                    TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }
            return TablaHtml.ToString();
        }


        public static string DevuelveBodyHtmlNormalEditable(DataTable origen)
        {

            StringBuilder TablaHtml = new StringBuilder();
            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");

                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {
                    if (cnt_columnas == 1)
                    {
                        TablaHtml.AppendLine("<td>" + "<a href='#' class='campoDesc editar editable editable-click editable-open' data-type='text'>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</a>" + "</td>");
                     
                    }
                    else if (cnt_columnas == 2)
                    {

                        if (Convert.ToBoolean(origen.Rows[cnt_filas]["INACTIVO"]) == false)
                        {
                             TablaHtml.AppendLine("<td><input data-toggle='toggle' class='sel_activo' type='checkbox'  checked /></td>");
                        }
                        else
                        {
                            TablaHtml.AppendLine("<td><input data-toggle='toggle' class='sel_activo' type='checkbox'  /></td>");
                        }            
                     
                    }
                    else
                    {
                        TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                    }
                
                }
                TablaHtml.AppendLine("</tr>");
            }
            return TablaHtml.ToString();
        }




        public static string DevuelveBodyPerfilesUSHtmlNormal(DataTable origen)
        {

            StringBuilder TablaHtml = new StringBuilder();
            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");
                if (origen.Rows[cnt_filas]["ASIGNADO"].ToString() == "S")
                {
                    TablaHtml.AppendLine("<td><input data-toggle='toggle' class='sel_chk' type='checkbox' name='miCheck' checked data-permiso ='1'/></td>");
                    //  TablaHtml.AppendLine("<td><div class='sel_chk' name='miCheck' onClick='marcar(" + cnt_filas+ ")'/></td>");
                    //TablaHtml.AppendLine("<td><div class='sel_chk'/></td>");

                }
                else
                {

                    TablaHtml.AppendLine("<td><input data-toggle='toggle' class='sel_chk' type='checkbox' name='miCheck'  data-permiso ='0'/></td>");

                }

                
                for (int cnt_columnas = 1; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {             

                    TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }
            return TablaHtml.ToString();
        }



        public static string DevuelveBodyLocacionUSHtmlNormal(DataTable origen)
        {

            StringBuilder TablaHtml = new StringBuilder();
            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {
                TablaHtml.AppendLine("<tr>");
                if (origen.Rows[cnt_filas]["ASIGNADO"].ToString() == "S")
                {
                    TablaHtml.AppendLine("<td><input data-toggle='toggle' class='sel_chk_1' type='checkbox' name='miCheck' checked data-permiso ='1'/></td>");
                   
                }
                else
                {

                    TablaHtml.AppendLine("<td><input data-toggle='toggle' class='sel_chk_1' type='checkbox' name='miCheck'  data-permiso ='0'/></td>");

                }


                for (int cnt_columnas = 1; cnt_columnas < origen.Columns.Count; cnt_columnas++)
                {

                    TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }
            return TablaHtml.ToString();
        }




        public static string DevuelveBodyHtmlCheckPermiso(DataTable origen)
        {
                       
            StringBuilder TablaHtml = new StringBuilder();
            for (int cnt_filas = 0; cnt_filas < origen.Rows.Count; cnt_filas++)
            {

                if (origen.Rows[cnt_filas]["APROBADA"].ToString() == "SI")
                {
                    TablaHtml.AppendLine("<tr style='color:#fff;background-color:#5cb85c;border-color:#4cae4c'>");

                }
                else
                {
                    TablaHtml.AppendLine("<tr>");
                }                                                         

                if (origen.Rows[cnt_filas]["PERMISO"].ToString() == "1")
                {
                    TablaHtml.AppendLine("<td><input class='sel_chk' type='checkbox' name='miCheck' data-permiso ='1'/></td>");
                    
                }
                else
                {

                    TablaHtml.AppendLine("<td><input class='sel_chk' type='checkbox' name='miCheck' disabled data-permiso ='0' /></td>");
             
                }            

                for (int cnt_columnas = 0; cnt_columnas < origen.Columns.Count - 2; cnt_columnas++)
                {
                    TablaHtml.AppendLine("<td>" + origen.Rows[cnt_filas][cnt_columnas].ToString() + "</td>");
                }
                TablaHtml.AppendLine("</tr>");
            }

            return TablaHtml.ToString();

        }

                 
        public static string Exportarxml()
        {


            string fecha_ultima = string.Empty;
            fecha_ultima = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();



                StringBuilder exportar_xml = new StringBuilder();
                exportar_xml.AppendLine(agrega_encabezado_xml_excel());
                exportar_xml.AppendLine("<Table x:FullColumns=" + aplica_comilla("1") + " x:FullRows=" + aplica_comilla("1") + " ss:DefaultColumnWidth=" + aplica_comilla("60") + " ss:DefaultRowHeight=" + aplica_comilla("15") + ">");
             

                exportar_xml.AppendLine("<Row>");
                exportar_xml.AppendLine("<Cell  ss:StyleID=" + aplica_comilla("s67") + "><Data ss:Type=" + aplica_comilla("String") + ">" + "ID" + "</Data></Cell>");
                exportar_xml.AppendLine("<Cell  ss:StyleID=" + aplica_comilla("s67") + "><Data ss:Type=" + aplica_comilla("String") + ">" + "DESCRIPCION" + "</Data></Cell>");       
                exportar_xml.AppendLine("</Row>");
                
                
                exportar_xml.AppendLine("<Row>");
                exportar_xml.AppendLine("<Cell ss:StyleID=" + aplica_comilla("sLineas") + "><Data ss:Type=" + aplica_comilla("String") + ">" + "1" + "</Data></Cell>");
                exportar_xml.AppendLine("<Cell ss:StyleID=" + aplica_comilla("sLineas") + "><Data ss:Type=" + aplica_comilla("String") + ">" + "2" + "</Data></Cell>");
                exportar_xml.AppendLine("</Row>");
                                                             

                exportar_xml.AppendLine("</Table>");

                exportar_xml.AppendLine("<WorksheetOptions xmlns=" + aplica_comilla("urn:schemas-microsoft-com:office:excel") + ">");
                exportar_xml.AppendLine("<Unsynced/>");
                exportar_xml.AppendLine("<Selected/>");
                exportar_xml.AppendLine("</WorksheetOptions>");
                exportar_xml.AppendLine("</Worksheet>");

                                

                exportar_xml.AppendLine("</Workbook>");

                return exportar_xml.ToString();

                

        }


        public static string agrega_encabezado_xml_excel()
        {

            StringBuilder encabezado = new StringBuilder();
            encabezado.AppendLine("<?xml version=" + Convert.ToChar(34) + "1.0" + Convert.ToChar(34) + "?>");
            encabezado.AppendLine("<?mso-application progid=" + Convert.ToChar(34) + "Excel.Sheet" + Convert.ToChar(34) + "?>");
            encabezado.AppendLine("<Workbook xmlns=" + Convert.ToChar(34) + "urn:schemas-microsoft-com:office:spreadsheet" + Convert.ToChar(34));
            encabezado.AppendLine("xmlns:o = " + Convert.ToChar(34) + "urn:schemas-microsoft-com:office:office" + Convert.ToChar(34));
            encabezado.AppendLine("xmlns:x = " + Convert.ToChar(34) + "urn:schemas-microsoft-com:office:excel" + Convert.ToChar(34));
            encabezado.AppendLine("xmlns:ss = " + Convert.ToChar(34) + "urn:schemas-microsoft-com:office:spreadsheet" + Convert.ToChar(34));
            encabezado.AppendLine("xmlns:html=" + Convert.ToChar(34) + "http://www.w3.org/TR/REC-html40" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("<DocumentProperties xmlns=" + Convert.ToChar(34) + "urn:schemas-microsoft-com:office:office" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("<Version>14.00</Version>");
            encabezado.AppendLine("</DocumentProperties>");
            encabezado.AppendLine("<OfficeDocumentSettings xmlns=" + Convert.ToChar(34) + "urn:schemas-microsoft-com:office:office" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("<AllowPNG/>");
            encabezado.AppendLine("</OfficeDocumentSettings>");
            encabezado.AppendLine(" <Styles>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "Default" + Convert.ToChar(34) + " ss:Name=" + Convert.ToChar(34) + "Normal" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Alignment ss:Vertical=" + Convert.ToChar(34) + "Bottom" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Borders/>");
            encabezado.AppendLine("   <Font ss:FontName=" + Convert.ToChar(34) + "Calibri" + Convert.ToChar(34) + " x:Family=" + Convert.ToChar(34) + "Swiss" + Convert.ToChar(34) + " ss:Size=" + Convert.ToChar(34) + "11" + Convert.ToChar(34) + " ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Interior/>");
            encabezado.AppendLine("   <NumberFormat/>");
            encabezado.AppendLine("   <Protection/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "s62" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Alignment ss:Horizontal=" + Convert.ToChar(34) + "Left" + Convert.ToChar(34) + " ss:Vertical=" + Convert.ToChar(34) + "Bottom" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Borders/>");
            encabezado.AppendLine("   <Font ss:Bold=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + " ss:FontName=" + Convert.ToChar(34) + "Arial" + Convert.ToChar(34) + " ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Interior/>");
            encabezado.AppendLine("   <NumberFormat/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "s82" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Alignment ss:Vertical=" + Convert.ToChar(34) + "Bottom" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Borders/>");
            encabezado.AppendLine("   <Font ss:FontName=" + Convert.ToChar(34) + "Arial" + Convert.ToChar(34) + " x:Family=" + Convert.ToChar(34) + "Swiss" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Interior/>");
            encabezado.AppendLine("   <NumberFormat/>");
            encabezado.AppendLine("   <Protection/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "s104" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Alignment ss:Vertical=" + Convert.ToChar(34) + "Bottom" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Borders/>");
            encabezado.AppendLine("   <Font ss:FontName=" + Convert.ToChar(34) + "Arial" + Convert.ToChar(34) + " x:Family=" + Convert.ToChar(34) + "Swiss" + Convert.ToChar(34) + " ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Interior/>");
            encabezado.AppendLine("   <NumberFormat/>");
            encabezado.AppendLine("   <Protection/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "s105" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Borders/>");
            encabezado.AppendLine("   <Font ss:FontName=" + Convert.ToChar(34) + "Arial" + Convert.ToChar(34) + " x:Family=" + Convert.ToChar(34) + "Swiss" + Convert.ToChar(34) + " ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "s106" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Borders/>");
            encabezado.AppendLine("   <Font ss:FontName=" + Convert.ToChar(34) + "Arial" + Convert.ToChar(34) + " x:Family=" + Convert.ToChar(34) + "Swiss" + Convert.ToChar(34) + " ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <NumberFormat ss:Format=" + Convert.ToChar(34) + "Long Time" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "s108" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Alignment ss:Horizontal=" + Convert.ToChar(34) + "Center" + Convert.ToChar(34) + " ss:Vertical=" + Convert.ToChar(34) + "Bottom" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Borders>");
            encabezado.AppendLine("    <Border ss:Position=" + Convert.ToChar(34) + "Left" + Convert.ToChar(34) + " ss:LineStyle=" + Convert.ToChar(34) + "Continuous" + Convert.ToChar(34) + " ss:Weight=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + "");
            encabezado.AppendLine("     ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("    <Border ss:Position=" + Convert.ToChar(34) + "Right" + Convert.ToChar(34) + " ss:LineStyle=" + Convert.ToChar(34) + "Continuous" + Convert.ToChar(34) + " ss:Weight=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + "");
            encabezado.AppendLine("     ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("    <Border ss:Position=" + Convert.ToChar(34) + "Top" + Convert.ToChar(34) + " ss:LineStyle=" + Convert.ToChar(34) + "Continuous" + Convert.ToChar(34) + " ss:Weight=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + "");
            encabezado.AppendLine("     ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   </Borders>");
            encabezado.AppendLine("   <Font ss:FontName=" + Convert.ToChar(34) + "Arial" + Convert.ToChar(34) + " ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Interior ss:Color=" + Convert.ToChar(34) + "#C0C0C0" + Convert.ToChar(34) + " ss:Pattern=" + Convert.ToChar(34) + "Solid" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <NumberFormat/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + Convert.ToChar(34) + "s109" + Convert.ToChar(34) + ">");
            encabezado.AppendLine("   <Alignment ss:Vertical=" + Convert.ToChar(34) + "Bottom" + Convert.ToChar(34) + " ss:WrapText=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("   <Borders/>");
            encabezado.AppendLine("   <Font ss:FontName=" + Convert.ToChar(34) + "Arial" + Convert.ToChar(34) + " x:Family=" + Convert.ToChar(34) + "Swiss" + Convert.ToChar(34) + " ss:Color=" + Convert.ToChar(34) + "#000000" + Convert.ToChar(34) + "/>");
            encabezado.AppendLine("  </Style>");
            encabezado.AppendLine("  <Style ss:ID=" + aplica_comilla("s63") + ">");
            encabezado.AppendLine("   <Alignment ss:Horizontal=" + aplica_comilla("Center") + " ss:Vertical=" + aplica_comilla("Bottom") + "/></Style>");


            encabezado.AppendLine("<Style ss:ID=" + aplica_comilla("scentrado") + ">");
            encabezado.AppendLine(" <Alignment ss:Horizontal=" + aplica_comilla("Center") + " ss:Vertical=" + aplica_comilla("Center") + "/>");
            encabezado.AppendLine("</Style>");
            encabezado.AppendLine("<Style ss:ID=" + aplica_comilla("scentrado_color") + ">");
            encabezado.AppendLine(" <Alignment ss:Horizontal=" + aplica_comilla("Center") + " ss:Vertical=" + aplica_comilla("Center") + "/>");
            encabezado.AppendLine(" <Interior ss:Color=" + aplica_comilla("#FFFF00") + " ss:Pattern=" + aplica_comilla("Solid") + "/>");
            encabezado.AppendLine("</Style>");


            encabezado.AppendLine("<Style ss:ID=" + aplica_comilla("s67") + ">");
            encabezado.AppendLine("<Borders>");

            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Bottom") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Left") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Right") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Top") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");

            encabezado.AppendLine("</Borders>");
            encabezado.AppendLine("<Interior ss:Color=" + aplica_comilla("#D9D9D9") + " ss:Pattern=" + aplica_comilla("Solid") + "/>");

            encabezado.AppendLine("</Style>");



            encabezado.AppendLine("<Style ss:ID=" + aplica_comilla("sLineas") + ">");
            encabezado.AppendLine("<Borders>");

            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Bottom") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Left") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Right") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Top") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("</Borders>");
            encabezado.AppendLine("</Style>");


            encabezado.AppendLine("<Style ss:ID=" + aplica_comilla("sLineasCenter") + ">");
            encabezado.AppendLine("<Alignment ss:Horizontal=" + aplica_comilla("Center") + " ss:Vertical=" + aplica_comilla("Bottom") + "/>");


            encabezado.AppendLine("<Borders>");

            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Bottom") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Left") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Right") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("<Border ss:Position=" + aplica_comilla("Top") + " ss:LineStyle=" + aplica_comilla("Continuous") + " ss:Weight=" + aplica_comilla("1") + "/>");
            encabezado.AppendLine("</Borders>");
            encabezado.AppendLine("<Interior ss:Color=" + aplica_comilla("#D9D9D9") + " ss:Pattern=" + aplica_comilla("Solid") + "/>");


            encabezado.AppendLine("</Style>");


            

            encabezado.AppendLine(" </Styles>");
            encabezado.AppendLine("<Worksheet ss:Name=" + Convert.ToChar(34) + "Resumen" + Convert.ToChar(34) + ">");
            return encabezado.ToString();
        }

        public static string aplica_comilla(string texto)
        {

            return Convert.ToChar(34) + texto + Convert.ToChar(34);
        }

        public static Boolean RutValido(String rut)
        {

            rut = rut.Replace(".", "").ToUpper();
            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }
        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }
    }



}
