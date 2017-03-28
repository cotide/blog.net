/************************************************
* 功能说明: JQuery postForm plug-in 1.0
* 创建时间: 2012-11-22
*   创建人: xcli
* 作者网址：http://http://www.cotide.com/ 
*     描述: Post表单提交
/************************************************/
if (typeof cotide == "undefined") {
    cotide = new Object();
}

cotide.postForm = function () {
    return {
        // Post数据
        // url: Post目标地址
        // postKey: Post数据Key值 多位用,分隔
        // postData: Post数据值 多位用,分割
        // returnUrl: 转向URL
        post: function (
            url,
            postKey,
            postData,
            returnUrl) {
            //  debugger;
            var key = postKey.split(",");
            var value = postData.split(",");
            var par = "";
            $(key).each(function (index) {
                if (par != "") {
                    par = par + "&" + this + "=" + value[index];
                } else {
                    par = this + "=" + value[index];
                }
            });
            $.ajax({
                type: "POST",
                url: url,
                data: par,
                async: false,
                success: function (result) {
                    if (result != undefined && result.Data != undefined) {
                        if (result.Data.isOk == "true") {
                            // 操作成功
                            parent.cotide.dialog.msgbox("success", "操作成功");
                        } else {
                            parent.cotide.dialog.msgbox("error", "操作失败");
                        }
                        if (returnUrl != "") {
                            window.location.href = result.Data.returnUrl;
                        }
                    } else {
                        parent.cotide.dialog.msgbox("error", "未知的返回结果,请联系管理员");
                    }

                },
                error: function () { 
                    parent.cotide.dialog.msgbox("error", "系统异常请联系管理员");
                }
            });
        }
    };
} (); 