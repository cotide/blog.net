 /************************************************
* 功能说明: JQuery selAll plug-in 1.0
* 创建时间: 2012-11-22
*   创建人: xcli
* 作者网址：http://http://www.cotide.com/ 
*     描述: JQUERY 全选插件
/************************************************/
if (typeof cotide == "undefined") {
    cotide = new Object();
} 
/* 全选控制 */
cotide.selAll = function () {
    var selAllTag = "SelAll";
    var selTagName = "Sel";
    return {
        init: function () {
            var obj = $("input[tag='" + selAllTag + "']");
            obj.change(function () {
                var isSel = $("input[tag='" + selAllTag + "']").is(":checked"); 
                if (isSel) { 
                    $("input[tag='" + selTagName + "']").attr("checked", true);
                } else {
                    $("input[tag='" + selTagName + "']").removeAttr("checked");
                }
              
            });
        }
    };
} ();
/* 表格控制 */
cotide.table = function () { 
    return {
        init: function () {
            var tb = $("table[tbPanel='true'] tr:not(:first)");
            tb.hover(function () {
                tb.css("background-color", "");
                $(this).css("background-color", "#E9E2D8");
            });
        }
    };
} ();
$(function () {
    cotide.selAll.init();
    cotide.table.init();
});


