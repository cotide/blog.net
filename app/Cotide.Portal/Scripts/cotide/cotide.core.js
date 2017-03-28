String.guid = function () {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}
String.format = function (fmt) {
    var params = arguments;
    var pattern = /{{|{[1-9][0-9]*}|\x7B0\x7D/g;
    return fmt.replace(pattern,
    function (p) {
        if (p == "{{") return "{";
        return params[parseInt(p.substr(1, p.length - 2), 10) + 1];
    });
}
String.prototype.urlstamp = function () {
    var template = "{0}?_t={1}";
    if (this.indexOf("?") != -1) {
        template = "{0}&_t={1}";
    }
    return String.format(template, this, Date.parse(new Date()));
}
String.prototype.addurlpara = function (name, value) {
    var template = "{0}?{1}={2}";
    if (this.indexOf("?") != -1) {
        template = "{0}&{1}={2}";
    }
    return String.format(template, this, name, value);
}
String.isContent = function (str, tag) {
    if (str.indexOf(tag) >= 0) {
        return true;
    } else {
        return false;
    }
}
String.prototype.toDecimal2 = function () {
    var f = parseFloat(this);
    if (isNaN(f)) {
        return false;
    }
    var f = Math.round(this * 100) / 100;
    var s = f.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        rs = s.length;
        s += '.';
    }
    while (s.length <= rs + 2) {
        s += '0';
    }
    return s;
};
String.prototype.sub = function (n) {
    var r = /[^\x00-\xff]/g;
    if (this.replace(r, "mm").length <= n) return this;
    var m = Math.floor(n / 2);
    for (var i = m; i < this.length; i++) {
        if (this.substr(0, i).replace(r, "mm").length >= n) {
            return this.substr(0, i) + "...";
        }
    }
    return this;
};
String.prototype.trimStart = function (trimStr) {
    if (!trimStr) {
        return this;
    }
    var temp = this;
    while (true) {
        if (temp.substr(0, trimStr.length) != trimStr) {
            break;
        }
        temp = temp.substr(trimStr.length);
    }
    return temp;
};
String.prototype.trimEnd = function (trimStr) {
    if (!trimStr) {
        return this;
    }
    var temp = this;
    while (true) {
        if (temp.substr(temp.length - trimStr.length, trimStr.length) != trimStr) {
            break;
        }
        temp = temp.substr(0, temp.length - trimStr.length);
    }
    return temp;
};
String.prototype.trim = function (trimStr) {
    var temp = trimStr;
    if (!trimStr) {
        temp = " ";
    }
    return this.trimStart(temp).trimEnd(temp);
};
jQuery.fn.extend({
    AjaxPager: function () {
        var $This = $(this);
        var $PanelId = $This.attr("panelId");
        var $Panel = $("#" + $PanelId);
        if ($Panel.length > 0) {
            $This.find("a").click(function () {
                var url = $(this).attr("tag");
                if (url != undefined && url != null && url != "") {
                    $Panel.load(url.urlstamp());
                }
            });
        }
    },
    getFrameBodyHeight: function () {
        var objBody = $(this)[0].contentWindow.document.body || $(this)[0].contentDocument.body;
        return $(objBody).height();
    },
    setIFrameAuthHeight: function (isDialog, time, callback) {
        var $this = $(this);
        var oauthInterval = window.setInterval(function () {
            try {
                var height = $this.getFrameBodyHeight();
                $this.height(height);
                if (isDialog) {
                    $this.parent().height(height);
                }
                if ($.isFunction(callback)) {
                    callback(this, height);
                }
            } catch (ex) { }
        },
        time);
        $this.data("oauthInterval", oauthInterval);
    },
    setIFrameHeight: function (callback) {
        var $this = $(this);
        if ($.browser.msie) {
            $this.get(0).onreadystatechange = function () {
                if (this.readyState && this.readyState == "complete") {
                    var height = $this.getFrameBodyHeight();
                    $this.height(height + 60);
                    if ($.isFunction(callback)) {
                        callback(this, height);
                    }
                }
            }
        } else {
            $this.get(0).onload = function () {
                try {
                    if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                        var height = $this.getFrameBodyHeight();
                        $this.height(height + 60);
                        if ($.isFunction(callback)) {
                            callback(this, height);
                        }
                    }
                } catch (e) { }
            };
        }
    },
    setIFrameCompleteHeight: function (callback) {
        try {
            var $this = $(this);
            if ($.browser.msie) {
                $this.get(0).onreadystatechange = function () {
                    if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                        $this.show();
                        $this.setIFrameAuthHeight(200,
                        function (iframe, height) {
                            if ($.isFunction(callback)) {
                                callback(height);
                            }
                            $this.height(height);
                        });
                    }
                };
            } else {
                $this.get(0).onload = function () {
                    if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                        $this.show();
                        $this.setIFrameAuthHeight(200,
                        function (iframe, height) {
                            if ($.isFunction(callback)) {
                                callback(height);
                            }
                            $this.height(height);
                        });
                    }
                };
            }
        } catch (ex) { }
    },
    ajaxLoadData: function (url) {
        var loadUrl = url;
        $(this).load(loadUrl.urlstamp());
    },
    ajaxPostForm: function (validatorFun, successAfterFun, errorAfterFun) {
        var $Form = $(this);
        var $FormSubmitBtn = $Form.find("button[type='submit']");
        $Form.submit(function () {
            var isCheck = false;
            if ($.isFunction(validatorFun)) {
                isCheck = validatorFun();
            }
            if (isCheck) {
                var url = $Form.attr("action");
                if (url != undefined && url != null && url != "") {
                    if ($FormSubmitBtn.length > 0) { 
                        $FormSubmitBtn.attr("disabled", "disabled");
                    }
                    $.ajax({
                        url: url.urlstamp(),
                        type: $Form.attr("method"),
                        data: $Form.serialize(),
                        dataType: 'json',
                        cache: false,
                        success: function (result) {
                            if ($FormSubmitBtn.length > 0) {
                                $FormSubmitBtn.removeAttr("disabled");
                            }
                            if (result.Success) {
                                if ($.isFunction(successAfterFun)) {
                                    successAfterFun(result);
                                }
                            } else {
                                if ($.isFunction(errorAfterFun)) {
                                    errorAfterFun(result);
                                } else {
                                    alert('评论失败，请联系管理员');
                                }
                            }
                        },
                        error: function (ex) {
                            if ($FormSubmitBtn.length > 0) {
                                $FormSubmitBtn.removeAttr("disabled");
                            }
                            if ($.isFunction(errorAfterFun)) {
                                errorAfterFun(ex);
                            } else {
                                alert('评论失败，请联系管理员');
                            }
                        }
                    });
                }
            }
            return false;
        });
    }
});