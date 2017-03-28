/************************************************/
//功能说明:用于顶层有iframe的页面，用于支撑模式层弹出窗
//创建时间:2010-5-6
//创建人:ysmo
/************************************************/
if (typeof ens == "undefined") {
    ens = new Object();
}

ens.dialog = function () {
    var _data;
    return {

        /* 
        ens.dialog.open(url,title,width,height,callback){
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
                element = $(String.format("<div id=\"{0}\" class=\"ens-ui-dialog\" ><div id=\"overlay\" style=\"width:1000px;height:1000px;position:absolute;top:0px;left:0px;display:none;\"></div><div class=\"ajaxloading\" style=\"width:100px;margin:10px auto;\">正在加载...</div><iframe name=\"{0}\" id=\"iframe_{0}\"  name=\"dialogiframe\" width='100%' src=\"{1}\"  height=\"{2}\" frameborder=\"0\" style=\"display:none\"  scrolling=\"no\" ></iframe></div>", id, url.urlstamp(), height + "px"));
                var $frame = $("iframe", element);
                $frame.data("oauthInterval", null);
                element.data("returnValue", null);
                var options = { modal: true, position: "center" };
                options.title = title;
                options.type = "url";
                options.width = width;
                options.resizable = false;
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
                if ($.browser.msie) {
                    $frame.get(0).onreadystatechange = function () {
                        if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                            $(".ajaxloading", element).hide();
                            $(this).show();
                            if ($frame.data("oauthInterval") == null) {
                                $frame.setIFrameAuthHeight(200, function (event, height) {
                                    if (typeof height != "undefined" && $frame.data("height") != height) {
                                        $frame.data("height", height);
                                        if ($.browser.msie && parseInt($.browser.version, 10) < 8) {
                                            element.dialog("option", "height", height + 15);
                                        } else {
                                            element.dialog("option", "height", height + 25);
                                        }
                                        //element.dialog("option", "position", 'center');
                                    }
                                });
                            }
                            try {
                                document.frames[id].ens.dialog.setdialog(element);
                            } catch (ex) { }
                        }
                    }
                } else {
                    //firefox支持
                    $frame.get(0).onload = function () {
                        try {
                            if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                                $(".ajaxloading", element).hide();
                                $(this).show();
                                if ($frame.data("oauthInterval") == null) {
                                    $frame.setIFrameAuthHeight(200, function (event, height) {
                                        if ($frame.data("height") != height) {
                                            $frame.data("height", height);
                                            element.dialog("option", "height", height + 15);
                                            //element.dialog("option", "position", 'center');
                                        }
                                    });
                                }
                                try {
                                    var ele_iframe = $("iframe", element).get(0);
                                    ele_iframe.contentWindow.ens.dialog.setdialog(element);
                                } catch (ex) { }
                            }
                        } catch (e) { }
                    }
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
                element.dialog("option", "height", height + 15);
                //element.dialog("option", "position", 'center');
                element.dialog("option", "close", function (event, ui) {
                    if ($.isFunction(callback)) {
                        callback(element.data("returnValue"));
                    }
                    window.clearInterval($frame.data("oauthInterval"));
                    $frame.data("oauthInterval", null);
                }).dialog("open");
            }

        },
        /*
        ens.dialog.msgbox("类型:成功消息success、失败消息error、普通消息info","提示内容")
        */
        msgbox: function (type, msg, callback) {
            var element = new Object();
            var id = String.guid();
            switch (type.toLowerCase()) {
                case "success":
                    if (undefined == msg || "" == msg || null == msg) {
                        msg = "保存成功！";
                    }
                    element = $(String.format('<div class="pop_tip" id="{0}"><div class="left_tip"></div><div class="right_tip"><span class="dialogDot_correct"></span>{1}</div></div>', id, msg));
                    break;
                case "error":
                    if (undefined == msg || "" == msg || null == msg) {
                        msg = "保存失败！";
                    }
                    element = $(String.format('<div class="pop_tip" id="{0}"><div class="left_tip"></div><div class="right_tip"><span class="dialogDot_wrong"></span>{1}</div></div>', id, msg));
                    break;
                case "loading":
                    if (undefined == msg || "" == msg || null == msg) {
                        msg = "加载中...";
                    }
                    id = "sysloading";
                    element = $(String.format('<div class="pop_tip" id="{0}"><div class="left_tip"></div><div class="right_tip"><span class="dialogDot_loading"></span>{1}</div></div>', id, msg));
                    break;
                default:
                    element = $(String.format('<div class="pop_tip" id="{0}"><div class="left_tip"></div><div class="right_tip"><span class="dialogDot_warn"></span>{1}</div></div>', id, msg));
                    break;
            }
            $("body").append(element);

            var boxwidth = $("#" + id).width();
            var boxheight = $("#" + id).height();
            var dLeft = ($(window).width() - parseInt(boxwidth)) / 2;
            var dTop = ($(window).height() - parseInt(boxheight)) / 2 + parseInt($(document).scrollTop());
            $("#" + id).css("display", "none").css({ "position": "absolute", "top": dTop, "left": dLeft }).fadeIn("fast");

            if (type.toLowerCase() == "loading") {
                //                window.setTimeout(function () {
                //                    $("#" + id).fadeOut("slow", function () {
                //                        $("#" + id).remove();
                //                        if ($.isFunction(callback)) {
                //                            callback(element);
                //                        }
                //                    })

                //                }, 8000);
                return;
            } else {
                window.setTimeout(function () {
                    $("#" + id).fadeOut("slow", function () {
                        $("#" + id).remove();
                        if ($.isFunction(callback)) {
                            callback(element);
                        }
                    })

                }, 1000);
            }
        },
        /* 
        ens.dialog.alert("消息内容字符串","标题",回调方法);  
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
        /* 
        * 消息即信窗
        * ens.dialog.popupMsg("消息标题","消息内容","消息链接")
        */
        popupMsg: function (title, message, link) {
            var element = new Object();
            var id = String.guid();
            var windowHeight;
            if (window.innerHeight) {
                windowHeight = window.innerHeight;
            }
            else if (document.documentElement && document.documentElement.clientHeight) {
                windowHeight = document.documentElement.clientHeight;
            }
            else if (document.body) {
                windowHeight = document.body.clientHeight;
            }

            var fnClose = function () {
                var msgbox = $("#" + id);
                msgbox.animate({ top: windowHeight }, 'slow', null, function () { $(this).remove(); });
                return false;
            }
            element = $(String.format("<div class=\"im_box dev_popup_msgbox\" id=\"{0}\" style=\"display:none\"><div class=\"title\"><span class=\"fL\"><span class=\"icon_imInfo\"></span>消息</span><a href=\"javascript:;\" class=\"fR\" onClick=\"$('#" + id + "').hide()\"><span class=\"icon_GrayClose\"></span></a></div><div class=\"im_con haveBg\"><h3>{1}</h3><p><a href=\"{3}\" target=\"MainFrame\">{2}</a></p></div></div>", id, title, message.sub(142), link));
            $("body").append(element);
            $("#" + id).css('top', windowHeight).show().animate({ top: windowHeight - 168 }, 'slow');

            var _close = setTimeout(fnClose, 5000);
            $("#" + id).hover(function () { clearTimeout(_close) }, function () { fnClose() });
        },
        //引导
        guide: function (options) {
            //控制滚动条位置
            window.scrollTo(0, 0);

            var o = {
                referenceId: "Reference",
                tagId: "",
                tagPosition: null,
                showId: "",
                showPosition: null,
                nextBtn: $(".guide_btnNext"),
                nextStep: null,
                stopBtn: $(".guide_btnClose"),
                callback: null
            };
            $.extend(o, options);

            //处理定位为相对定位
            var rL = $("#" + o.referenceId).offset().left;
            var rT = $("#" + o.referenceId).offset().top;
            var tL = rL + o.tagPosition[0];
            var tT = rT + o.tagPosition[1];
            var sL = rL + o.showPosition[0];
            var sT = rT + o.showPosition[1];

            o.tagPosition = [tL, tT];
            o.showPosition = [sL, sT];

            //弹出目标层
            var $tag = $("#" + o.tagId);
            var tagHeight = $tag.height();
            var tagWidth = $tag.width();
            $tag.dialog({
                dialogClass: "transparent",
                resizable: false,
                draggable: false,
                autoOpen: true,
                modal: true,
                height: tagHeight,
                width: tagWidth,
                position: o.tagPosition
            });
            //弹出引导层
            var $show = $("#" + o.showId);
            var showHeight = $show.height();
            var showWidth = $show.width();
            $show.dialog({
                dialogClass: "transparent",
                resizable: false,
                draggable: false,
                autoOpen: true,
                modal: false,
                height: showHeight,
                width: showWidth,
                position: o.showPosition
            });
            //下一步
            o.nextBtn.unbind("click");
            o.nextBtn.click(function () {
                $tag.dialog("close");
                $show.dialog("close");
                if ($.isFunction(o.nextStep)) {
                    o.nextStep();
                }
            })

            //关闭引导
            o.stopBtn.unbind("click");
            o.stopBtn.click(function () {
                $tag.dialog("close");
                $show.dialog("close");
                if ($.isFunction(o.callback)) {
                    o.callback();
                }
            })
        },
        /* 
        ens.dialog.warn("消息内容字符串","标题",回调方法);  
        */
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
            element = $(String.format("<div class=\"tips_detail txtCenter\"><p><span id=\"{0}\" class=\"dialogDot_warn\">{1}</span></p><div class=\"clearfix\"></div></div>", id, content));
            var dialogElement = element.dialog(options);
            dialogElement.parent().addClass("testClasss");
            return element;

        },
        /* 
        ens.dialog.confirm("消息内容字符串","标题",回调方法[参数true/false]);  
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
            })
            $("#close_" + id, element).click(function () {
                dialogElement.dialog('close');
                callback(false);
            })
            dialogElement.parent().addClass("testClasss");
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
