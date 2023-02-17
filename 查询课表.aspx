<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" enableEventValidation ="false" CodeFile="查询课表.aspx.cs" Inherits="查询课表" %>

<%@ Register src="WebUserControl_查询排课信息.ascx" tagname="WebUserControl_查询排课信息" tagprefix="uc1" %>

<%@ Register src="WebUserControl_查询教学任务.ascx" tagname="WebUserControl_查询教学任务" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script type="text/javascript" >

    var tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><meta charset="UTF-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->' +
    ' <style type="text/css">' +
    '.excelTable  {' +
    'border-collapse:collapse;' +
    ' border:thin solid #999; ' +
    '}' +
    '   .excelTable  th {' +
    '   border: thin solid #999;' +
    '  padding:20px;' +
    '  text-align: center;' +
    '  border-top: thin solid #999;' +
    ' ' +
    '  }' +
    ' .excelTable  td{' +
    ' border:thin solid #999;' +
    '  padding:2px 5px;' +
    '  text-align: center;' +
    ' }</style>' + '</head><body><table border="1">{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        return function (table, name) {
            if (!table.nodeType) table = document.getElementById(table)
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML };
            var downloadLink = document.createElement("a");
            downloadLink.href = uri + base64(format(template, ctx));
            downloadLink.download = name + ".xls";
            document.body.appendChild(downloadLink);
            downloadLink.click();
            document.body.removeChild(downloadLink);
           
        }
    })();

   

    /**
    * 合并单元格(如果结束行传0代表合并所有行)
    * @param table1    表格的ID
    * @param startRow  起始行
    * @param endRow    结束行
    * @param col   合并的列号，对第几列进行合并(从0开始)。第一行从0开始
    */
    function mergeCell(table1, startRow, endRow, col) {
        var tb = document.getElementById(table1);
        if (!tb || !tb.rows || tb.rows.length <= 0) {
            return;
        }
        if (col >= tb.rows[0].cells.length || (startRow >= endRow && endRow != 0)) {
            return;
        }
        if (endRow == 0) {
            endRow = tb.rows.length - 1;
        }
        for (var i = startRow; i < endRow; i++) {
            var tableInfo = tb.rows[startRow].cells[col].innerHTML;
            tableInfo = tableInfo.replace(/&nbsp;/ig, "");
            if (tableInfo!="" && tb.rows[startRow].cells[col].innerHTML == tb.rows[i + 1].cells[col].innerHTML) { //如果相等就合并单元格,合并之后跳过下一行
                tb.rows[i + 1].removeChild(tb.rows[i + 1].cells[col]);
                tb.rows[startRow].cells[col].rowSpan = (tb.rows[startRow].cells[col].rowSpan) + 1;
            } else {
                mergeCell(table1, i + 1, endRow, col);
                break;
            }
        }
    }
function ChangeTable() {
    var tableObj = document.getElementById("MainContent_GridView_课表");

    for (var i = 0; i < tableObj.rows.length; i++) {    //遍历Table的所有Row
        for (var j = 0; j < tableObj.rows[i].cells.length; j++) {   //遍历Row中的每一列
            try {
                tableInfo = tableObj.rows[i].cells[j].innerText;   //获取Table中单元格的内容
            }
            catch (err) {
                alert(tableInfo);
            }
            tableObj.rows[i].cells[j].innerText = "";
            tableObj.rows[i].cells[j].innerHTML = "";
            tableObj.rows[i].cells[j].innerHTML = tableInfo;

        }
    }
    for (i = 9; i >= 0;i--) //合并单元格
        mergeCell("MainContent_GridView_课表", 0, 0, i);
   
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">  
    <asp:SqlDataSource ID="SqlDataSource_学生信息" runat="server" 
        ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
        SelectCommand="SELECT * FROM [View_学生名单] WHERE [课程计划ID] = @课程计划ID">
        <SelectParameters>
            <asp:ControlParameter ControlID="HiddenField_课程计划ID" Name="课程计划ID" 
                PropertyName="Value" Type="Int32" />            
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="HiddenField_课程计划ID" runat="server" />
    <uc1:WebUserControl_查询排课信息 ID="WebUserControl_查询排课信息1" runat="server" OnClick="Button_Click" OnDropdownlist_changed="Dropdownlist_changed"/>
      
    <div class="clear"></div>
    <br>
    <div  style="float:right;" runat="server" id="div_成绩模板" Visible=false>
           &nbsp;<asp:Button ID="Button2" runat="server" Text="导出课表"   
               OnClientClick="tableToExcel('MainContent_GridView_课表','123');return false;" />
   </div>
    <br/>
    <asp:GridView ID="GridView_课表" runat="server" 
        CssClass="classtable" Caption="课表">
        <EmptyDataTemplate>
            没有数据
        </EmptyDataTemplate>
    </asp:GridView>
   
</asp:Content>

