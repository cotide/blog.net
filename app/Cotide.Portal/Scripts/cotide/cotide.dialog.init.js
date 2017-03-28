/************************************************
* 功能说明: JQuery dialog plug-in 1.0
* 创建时间: 2012-11-22
*   创建人: xcli
* 作者网址：http://http://www.cotide.com/ 
*     描述: JQUERY 弹出窗
/************************************************/
if (typeof ens == "undefined") {
    cotide = new Object();
} 
/*------------------------------------------------弹出窗控件-----------------------------------------*/
cotide.dialog = function () {
    var _data;
    return {
        /*
        显示结果提示
        cotide.dialog.showResultMsg(msg,type)
        panelId: 面板容器ID
        msg: 消息内容
        msgType: 类型 (error:错误提示,success:成功提示,info:消息提示) 默认info提示
        time: 提示销毁时间 默认为5秒
        */
        showResultMsg: function (
            panelId,
            msg,
            msgType,
            time) {
            // 容器 
            $This = $("#" + panelId);

            // 销毁时间
            var timeValue = 5000;
            if (time != undefined && time > 1000) {
                timeValue = time;
            }
            // 提示内容
            var main_template = "<div id=\"replyMsg\" class=\" alert {0} \"  > <strong>{1}</strong> {2} </div>";
            var result = "";
            if (msgType == "error") {
                // 错误提示
                result = String.format(main_template, "alert-warning", "提示", msg);
            } else if (msgType == "success") {
                // 成功提示
                result = String.format(main_template, "alert-success", "提示", msg);
            } else if (msgType == "info") {
                // 信息提示
                result = String.format(main_template, "alert-info", "提示", msg);
            }
            $This.empty();
            $This.append(result); 
            setTimeout(function () {
                $This.empty();
            }, timeValue);
        },
        /* 
        cotide.dialog.open(url,title,width,height,callback);
        */
        open: function (url, title, width, height, callback) {
            if (typeof width == "undefined") {
                width = 400;
            }
            if (typeof height == "undefined") {
                height = 200;
            }
            var element = $("div.ens-ui-dialog:hidden");
            if (element.length == 0) {
                var id = String.guid();
                element = $(String.format("<div id=\"{0}\" class=\"ens-ui-dialog\" ><div id=\"overlay\" style=\"width:1000px;height:1000px;position:absolute;top:0px;left:0px;display:none;\"></div><div class=\"ajaxloading\" style=\"width:150px;margin:10px auto;\">请稍等,数据加载中...</div><iframe name=\"{0}\" id=\"iframe_{0}\"  name=\"dialogiframe\" width='100%' src=\"{1}\"  height=\"{2}\" frameborder=\"0\" style=\"display:none\"  scrolling=\"no\" ></iframe></div>", id, url.urlstamp(), height + "px"));
                var $frame = $("iframe", element);
                $frame.data("oauthInterval", null);
                element.data("returnValue", null);
                var options = { modal: true, position: "center" };
                options.title = title;
                options.type = "url";
                options.width = width;
                options.draggable = false;
                options.resizable = true;
                options.height = height + 30;
                options.close = function (event, ui) {
                    if ($.isFunction(callback)) {
                        callback(element.data("returnValue"));
                    }
                    window.clearInterval($frame.data("oauthInterval"));
                    $frame.data("oauthInterval", null);
                };
                options.dragStart = function (event, ui) {
                    $("#overlay").show();
                };
                options.dragStop = function (event, ui) {
                    $("#overlay").hide();
                };
                $frame.data("height", height);
                if (jQuery.browser.msie) {
                    $frame.get(0).onreadystatechange = function () {
                        if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                            $(".ajaxloading", element).hide();
                            $(this).show();
                            $frame.setIFrameAuthHeight(true, 200);
                            //                            if ($frame.data("oauthInterval") == null) {
                            //                                                                $frame.setIFrameAuthHeight(200, function (event, height) {
                            //                                                                    if (typeof height != "undefined" && $frame.data("height") != height) {
                            //                                                                        $frame.data("height", height);
                            //                                                                        if ($.browser.msie && parseInt($.browser.version, 10) < 8) {
                            //                                                                            element.dialog("option", "height", height + 15);
                            //                                                                        } else {
                            //                                                                            element.dialog("option", "height", height + 25);
                            //                                                                        } 
                            //                                                                    }
                            //                                                                });
                            //                                
                            //                            }
                            try {
                                document.frames[id].ens.dialog.setdialog(element);
                            } catch (ex) { }
                        }
                    };
                } else {
                    //firefox支持   
                    $frame.get(0).onload = function () {
                        try {
                            if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                                $(".ajaxloading", element).hide();
                                $(this).show();
                                try {
                                    var ele_iframe = $("iframe", element).get(0);

                                    ele_iframe.contentWindow.ens.dialog.setdialog(element);
                                } catch (ex) { }
                            }
                        } catch (e) { }
                    };
                    $frame.setIFrameAuthHeight(true, 200);
                }
                var dialogElement = element.dialog(options);
                dialogElement.parent().addClass("testClasss");
            } else {
                element = element.first();
                var $frame = $("iframe", element);
                element.data("returnValue", null);
                $(".ajaxloading", element).show();
                $frame.hide().data("height", height).attr("src", url.urlstamp()).attr("height", height + "px");
                element.dialog("option", "width", width);
                element.dialog("option", "title", title);
                element.dialog("option", "draggable", false);
                element.dialog("option", "resizable", true);
                element.dialog("option", "height", height + 15);
                //element.dialog("option", "position", 'center');
                element.dialog("option", "close", function (event, ui) {
                    if ($.isFunction(callback)) {
                        callback(element.data("returnValue"));
                    }
                    window.clearInterval($frame.data("oauthInterval"));
                    $frame.data("oauthInterval", null);
                }).dialog("open");
                $frame.setIFrameAuthHeight(true, 200);
            }

        },
        /*
        cotide.dialog.msgbox("类型:成功消息success、失败消息error、普通消息info","提示内容")
        */
        msgbox: function (type, msg, callback, time) {
            var element = new Object();
            var id = String.guid();
            switch (type.toLowerCase()) {
                case "success":
                    if (undefined == msg || "" == msg || null == msg) {
                        msg = "保存成功！";
                    };
                    element = $(String.format('<div class="pop_tip" id="{0}"><div class="left_tip"></div><div class="right_tip"><span class="dialogDot_correct"></span>{1}</div></div>', id, msg));
                    break;
                case "error":
                    if (undefined == msg || "" == msg || null == msg) {
                        msg = "保存失败！";
                    };
                    element = $(String.format('<div class="pop_tip" id="{0}"><div class="left_tip"></div><div class="right_tip"><span class="dialogDot_wrong"></span>{1}</div></div>', id, msg));
                    break;
                default:
                    element = $(String.format('<div class="pop_tip" id="{0}"><div class="left_tip"></div><div class="right_tip"><span class="dialogDot_warn"></span>{1}</div></div>', id, msg));
                    break;
            }
            if ($("#" + id).length > 0) {
                element = $("#" + id);
            } else {
                $("body").append(element);
            }

            var boxwidth = $("#" + id).width();
            var boxheight = $("#" + id).height();
            var dLeft = ($(window).width() - parseInt(boxwidth)) / 2;
            var dTop = ($(window).height() - parseInt(boxheight)) / 2 + parseInt($(document).scrollTop());
            $("#" + id).css("display", "none").css({ "position": "absolute", "top": dTop, "left": dLeft }).fadeIn("fast");


            if (undefined == time || "" == time || null == time) {
                time = 1000;
            }
            window.setTimeout(function () {
                $("#" + id).fadeOut("slow", function () {
                    $("#" + id).remove();
                    if ($.isFunction(callback)) {
                        callback(element);
                    }
                });

            }, time);
        },
        /* 
        cotide.dialog.alert("消息内容字符串","标题",回调方法);  
        */
        alert: function (msg, title, callback) {
            var element = new Object();
            var id = String.guid();
            var content = "";
            var options = new Object();
            content = msg;
            options = {
                title: "消息提示",
                modal: true,
                resizable: false,
                buttons: {
                    确定: function () {
                        $(this).dialog('close');
                        if ($.isFunction(callback)) {
                            callback(element);
                        }
                    }
                },
                close: function (event, ui) {
                    if ($.isFunction(callback)) {
                        callback(element);
                    }
                }
            };
            element = $(String.format("<div id=\"{0}\" class=\"tips_detail\">{1}</div>", id, content));
            var dialogElement = element.dialog(options);
            dialogElement.parent().addClass("testClasss");
            return element;
        },
        warn: function (msg, title, callback) {
            var element = new Object();
            var id = String.guid();
            var content = "";
            var options = new Object();
            content = msg;
            options = {
                title: "消息提示",
                modal: true,
                resizable: false,
                buttons: {
                    确定: function () {
                        $(this).dialog('close');
                        if ($.isFunction(callback)) {
                            callback(element);
                        }
                    }
                },
                close: function (event, ui) {
                    if ($.isFunction(callback)) {
                        callback(element);
                    }
                }
            };
            element = $(String.format("<div class=\"tips_detail txtCenter\"><p><span id=\"{0}\" class=\"dialogDot_tip\">{1}</span></p><div class=\"clearfix\"></div></div>", id, content));
            var dialogElement = element.dialog(options);
            dialogElement.parent().addClass("testClasss");
            return element;

        },
        /* 
        cotide.dialog.confirm("消息内容字符串","标题",回调方法[参数true/false]);  
        */
        confirm: function (msg, title, callback) {
            var element = new Object();
            var id = String.guid();
            var content = "";
            var options = new Object();

            content = msg;
            var callback = callback;
            options = {
                title: title,
                modal: true,
                resizable: false,
                draggable: true,
                close: function (event, ui) {
                    callback(false);
                }
            };
            element = $(String.format("<div id=\"{0}\" style=\"text-align:center;\"><span class=\"dialogDot_answer\" >{1}</span><div class=\"clearfix\"></div><div class=\"smallbtn_control\"><input type=\"button\" id=\"ok_{0}\" value=\"确定\" class=\"btn_BlueSmall\" /><input type=\"button\" value=\"取消\" id=\"close_{0}\" class=\"btn_GraySmall\" /></div></div>", id, content));

            var dialogElement = element.dialog(options);

            $("#ok_" + id, element).click(function () {
                dialogElement.dialog('close');
                callback(true);
            });
            $("#close_" + id, element).click(function () {
                dialogElement.dialog('close');
                callback(false);
            });
            dialogElement.parent().addClass("testClasss");
            dialogElement.parent().append("<iframe width='100%' height='100%' frameborder='0' style='position:absolute; top:0px; z-index:-1; border-style:none;'></iframe>");
            return element;

        },
        data: function () {
            if (_data == undefined) {
                _data = {};
            }
            return _data;
        }
    };
} ();
