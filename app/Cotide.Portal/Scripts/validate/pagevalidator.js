/*
//------------------------------------------------------------------- 
//系统名称：验证控件脚本
//文件名称：pagevalidator.js
//模块名称：PageValidator 1.0 
//模块编号：
//作　　者：LHC
//完成日期：2010/8/29 14:34:00
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：  
//修改时间：
//修改内容：
//------------------------------------------------------------------- 
*/


// *************** 类构造参数(验证基类) ***************
// srcID ------------------------ 验证控件唯一标识
// empty ------------------------ 是否允许为空 true为允许 false为不允许
// msgOnTip --------------------- 默认验证提示
// msgOnErr --------------------- 错误提示
// msgOnEmpty ------------------- 空提示
// onValid ---------------------- 是否有效(暂时没用)
// ****************************************************
function Validator(srcID, empty, msgOnTip, msgOnErr, msgOnEmpty, onValid) {
    //默认验证提示
    this.msgOnTip = msgOnTip;
    //错误提示
    this.msgOnError = msgOnErr;
    //空提示
    this.msgOnEmpty = msgOnEmpty;
    //验证对象唯一标识
    this._parent = srcID;
    //是否允许为空
    this.empty = empty;
    //是否有效
    if (null != onValid && undefined != onValid)
        this.onValid = onValid;
    // 默认验证不通过
    this.isValid = false;
}


// *************** 验证类信息显示容器属性(容器格式为该验证唯一标识+'Tip') ***************
// srcID ------------------------ 验证控件唯一标识
// empty ------------------------ 是否允许为空 true为允许 false为不允许
// msgOnTip --------------------- 默认验证提示
// msgOnErr --------------------- 错误提示
// msgOnEmpty ------------------- 空描述
// onValid ---------------------- 是否有效
// **************************************************************************************
Validator.prototype.updateShow = function () {

    // 当前验证标识对象 
    var srcCTLWithJQuery = $("#" + this._parent);
    // 当前验证信息显示容器对象
    var tip = $("#" + srcCTLWithJQuery.attr("id") + "Tip");

    // 判断验证是否通过
    if (!this.isValid) {

        // 显示错误信息
        tip.html(this.msgOnError);
        // 清除错误样式
        tip.removeClass();
        // 加载错误信息样式
        tip.addClass("msgError");
    }
    else {

        // 通过验证
        
        if (this.empty && srcCTLWithJQuery.val().length == 0) {

            tip.html(this.msgOnEmpty);
        
        }
        else {
            
            tip.html(srcCTLWithJQuery.attr("alt"));
        }
        // 清除正确样式
        tip.removeClass();
        // 加载正确样式
        tip.addClass("msgOK");
    }
}

//////////////////////////////////////////////// 验证方案扩展 ////////////////////////////////////////////////

// *************** 下拉列表验证方式(继承验证基类) ***************
// srcID ------------------------ 验证控件唯一标识
// empty ------------------------ 是否允许为空 true为允许 false为不允许 
// msgOnErr --------------------- 错误提示
// msgOnEmpty ------------------- 空描述
// onValid ---------------------- 是否有效
// **************************************************************
function SelectValidator(srcID, empty, msgOnErr, msgOnEmpty, onValid) {

    // 指定继承关系
    this._base = Validator;
    // 调用父类构造方法
    this._base(srcID, empty, null, msgOnErr, msgOnEmpty, onValid);
    // 为空则验证通过

    if (empty) {
        this.isValid = true;
    }
}

// 指定验证显示效果为基类实现
SelectValidator.prototype.updateShow = Validator.prototype.updateShow;

// 下拉列表验证方式
SelectValidator.prototype.valid = function () {

    // 为空则验证通过
    if (this.empty)
        this.isValid = true;
    else {
        // 进行下来列表验证规则
        var isValid = true;
        // 获取当前验证对象
        var srcCTLWithJQuery = $("#" + this._parent);
        // 追加特有属性
        var groupname = srcCTLWithJQuery.attr("groupname");
        // 获取该特有属性对象
        var ctls = $("select[groupname='" + groupname + "']");
        // 循环判断是否已选择项
        ctls.each(function () {
        if (this.options.length > 0) {
                if (this.value == "" || this.value == "-1"||this.value =="0")
                    isValid = false;
                else
                    isValid = isValid && true;
            }
            else {
                isValid = isValid && true;
            }
        });
        this.isValid = isValid;
    }
}

// *************** 输入内容验证(继承验证基类) ***************
// srcID ------------------------ 验证控件唯一标识
// min -------------------------- 最小字符长度值
// max -------------------------- 最大字符长度值 
// empty ------------------------ 是否允许为空 true为允许 false为不允许 
// regex ------------------------ 正则表达式
// msgOnTip --------------------- 默认信息
// msgOnErr --------------------- 错误提示
// msgOnEmpty ------------------- 空描述
// onValid ---------------------- 是否有效
// **************************************************************
function InputValidator(srcID, min, max, empty, regex, msgOnTip, msgOnErr, msgOnEmpty, onValid) {

    // 指定继承关系
    this._base = Validator;
    // 调用父类构造方法
    this._base(srcID, empty, msgOnTip, msgOnErr, msgOnEmpty, onValid);
    // 当前最小字符长度值
    this.min = min;
    // 当前最大字符长度值
    this.max = max;
    // 正则表达式
    this.regex = regex;
    // 允许为空验证通过
    if (empty) {
        this.isValid = true;
    }
}
// 指定验证显示效果为基类实现
InputValidator.prototype.updateShow = Validator.prototype.updateShow;
// 输入内容验证方式
InputValidator.prototype.valid = function () {
    // 获取当前验证对象
    var srcCTLWithJQuery = $("#" + this._parent);
    // 当前对象值
    var val = srcCTLWithJQuery.val();
    // 长度
    var len = 0;

    // 如果输入框不能为空且没有输入任何内容，则直接验证失败
    if (!this.empty && (val.length == 0)) {
        this.isValid = false;
        return;
    }

    //进行长度检查
    for (var i = 0; i < val.length; i++) {
        // 如果是汉字则占两字节(通过判断ASCII码)
        if (val.charCodeAt(i) >= 0x4e00 && val.charCodeAt(i) <= 0x9fa5)
            len += 2;
        else
            len++;
    }
    // 统计结果
    if (val.length == 0 && this.empty == true) {
        this.isValid = true;
    }
    else if ((len < this.min) || ((this.max > 0) && (len > this.max))) {
        this.isValid = false;
    }

    //如果正则不为空,加入正则验证
    else if (this.regex != null && this.regex != undefined && typeof this.regex == "string" && this.regex != "") {
        var exp = new RegExp("^" + this.regex + "$", "i");
        // 直接将匹配结果赋值给isValid
        this.isValid = exp.test(val);
    }
    else {
        this.isValid = true;
    }
}



// *************** 内容对比验证类(继承验证基类) ***************
// srcID ------------------------ 验证控件唯一标识
// desID ------------------------ 对比验证控件唯一标识
// msgOnErr --------------------- 错误提示
// **************************************************************
function CompareValidator(srcID, desID, msgOnErr) {

    // 指定继承关系
    this.base = Validator;
    // 调用父类的构造方法
    this.base(srcID, true, null, msgOnErr, null);
    // 当前对比验证控件唯一标识
    this._compare = desID;
}
// 指定验证显示效果为基类实现
CompareValidator.prototype.updateShow = Validator.prototype.updateShow;
// 对比验证方式
CompareValidator.prototype.valid = function () {
    // 获取对比对象
    var srcCTLWithJQuery = $("#" + this._parent);
    var desCTLWithJQuery = $("#" + this._compare);
    // 判断属性是否相同
    this.isValid = (srcCTLWithJQuery.val() == desCTLWithJQuery.val());
}




// *************** 号码验证类(继承验证基类) ***************
// srcID ------------------------ 验证控件唯一标识
// minValue -------------------------- 最小字符长度值
// maxValue -------------------------- 最大字符长度值 
// msgOnErr --------------------- 错误提示
// **************************************************************
function NumberRangeValidator(srcID, minValue, maxValue, msgOnErr) {
    // 指定继承关系
    this.base = Validator;
    // 调用父类的构造方法
    this.base(srcID, true, null, msgOnErr, null);
    this._minValue = minValue;
    this._maxValue = maxValue;
}

// 指定验证显示效果为基类实现
NumberRangeValidator.prototype.updateShow = Validator.prototype.updateShow;
// 号码验证方式
NumberRangeValidator.prototype.valid = function () {
    // 获取验证控件对象
    var srcCTLWithJQuery = $("#" + this._parent); 
    if (srcCTLWithJQuery.val().length == 0) {
        this.isValid = true;
    }
    else {
        var num = parseInt(srcCTLWithJQuery.val());
        this.isValid = ((num >= this._minValue) && (num <= this._maxValue));
    }
}


// *************** 金额范围的验证类型(继承验证基类) 末完善 ***************
// srcID ----------------------------- 验证控件唯一标识
// minValue -------------------------- 最小字符长度值
// maxValue -------------------------- 最大字符长度值 
// msgOnErr -------------------------- 错误提示
// ***********************************************************************
function MoneyRangeValidator(srcID, minValue, maxValue, msgOnErr) {
    this.base = Validator;
    this.base(srcID, true, null, msgOnErr, null);

    this._minValue = minValue;
    this._maxValue = maxValue;
}
// 指定验证显示效果为基类实现
MoneyRangeValidator.prototype.updateShow = Validator.prototype.updateShow;
// 金额范围验证方式
MoneyRangeValidator.prototype.valid = function () {
    // 获取验证控件对象
    var srcCTLWithJQuery = $("#" + this._parent);
    if (srcCTLWithJQuery.val().length == 0) {
        this.isValid = true;
    }
    else {
        var num = parseFloat(srcCTLWithJQuery.val());
        this.isValid = ((num >= this._minValue) && (num <= this._maxValue));
    }
}



// *************** 异步Ajax验证(继承验证基类) 末完善 ***************
// srcID ----------------------------- 验证控件唯一标识
// url ------------------------------- 请求地址
// msgOnTip -------------------------- 鼠标移开验证焦点提示
// msgOnErr -------------------------- 错误信息 
// ajaxCallback ---------------------- 处理方法
// ***********************************************************************
function AjaxValidator(srcID, url, msgOnTip, msgOnErr, ajaxCallback) {
    this.base = Validator;
    this.base(srcID, true, null, msgOnErr, null);
    this.msgOnTip = msgOnTip;
    this.url = url;
    this.isAjax = true; 
    if (null != ajaxCallback)
        this.callback = ajaxCallback;
}
// 指定验证显示效果为基类实现
AjaxValidator.prototype.updateShow = Validator.prototype.updateShow;
// 异步验证方式
AjaxValidator.prototype.valid = function () {
    var srcCTLWithJQuery = $("#" + this._parent);
    var tip = $("#" + srcCTLWithJQuery.attr("id") + "Tip");

    tip.html("数据读取中，请稍后...");
    tip.removeClass();
    tip.addClass("msgAjaxing");

    srcCTLWithJQuery.get(0).ajaxvalid = this;
    $.ajax({
        type: "POST",
        url: this.url,
        data: srcCTLWithJQuery.attr("name") + "=" + srcCTLWithJQuery.val(),
        success: function (data) {
            //  var obj = eval("({value:" + data + "})");
            var t = srcCTLWithJQuery.get(0).ajaxvalid;

            if (undefined != t.callback) {
                t.callback(data, t);
            }
            else {
                if (data == 'true')
                    t.isValid = true;
                else
                    t.isValid = false;
            }
            if (t.msgOnEmpty == null) {
                 t.msgOnEmpty = srcCTLWithJQuery.attr("alt");
            }
            t.updateShow();
        }
    });
}

// ************************* 注册验证方式 **********************
// validator ----------------------------- 验证方式 
// *************************************************************
function initValid(validator) {
    // 获取当前验证对象唯一标识
    var srcID = validator._parent;
    // 当前验证对象
    var srcCTLWithJQuery = $("#" + srcID);
    // 验证标签
    var srcTag = srcCTLWithJQuery.get(0).tagName;
    // 缓存验证数组
    var arrayValidator = new Array();

    arrayValidator.push(validator);
    srcCTLWithJQuery.get(0).validator = arrayValidator;
    // 验证提示容器
    var tip = $("#" + srcID + "Tip");
    // 追加默认提示
    if (srcCTLWithJQuery.attr("description") != undefined) {
        tip.html(srcCTLWithJQuery.attr("description")); 
        // 追加默认提示样式
        tip.addClass("msgNormal");
    }
   


    if (srcTag == "INPUT" || srcTag == "TEXTAREA") {
        // 获取验证对象类型
        var type = srcCTLWithJQuery.attr("type");
        /// 文本类型
        if (type == "text" || type == "password" || type == "file" || srcTag == "TEXTAREA") {
            // 获取默认值
            var defaultVal = srcCTLWithJQuery.attr("value");
            // 默认值不等于null验证通过
            if (null != defaultVal && defaultVal != undefined && defaultVal != "") {
                validator.isValid = true;
            }
             
            if (validator.msgOnTip != null) {
                // 鼠标移开验证控件对象事件
                srcCTLWithJQuery.focus(function () {

                    // 获取验证提示容器
                    var tip = $("#" + this["id"] + "Tip");
                    // 追加鼠标移开验证控件提示
                    tip.html(this.validator[0].msgOnTip);
                    // 清楚样式	 
                    tip.removeClass();
                    // 加载鼠标移开控件对象样式
                    tip.addClass("msgOnFocus");
                });
            } 
            
            // 注册验证控件鼠标焦点失效时事件
            srcCTLWithJQuery.blur(function () {
                for (var i = 0; i < this.validator.length; i++) {
                    this.validator[i].valid(); 
                    // 不为AJAX验证操作，进行焦点失效提示
                    if (this.validator[i].isAjax == null || this.validator[i].isAjax == undefined) {
                        this.validator[i].updateShow();
                        if (!this.validator[i].isValid)
                            break;
                    }
                }
            });

            // 复选框|多选框
        } else if (type == "checkbox" || type == "radio") {
            // 获取当前对象
            var ctls = $("input[name=" + srcCTLWithJQuery.attr("name") + "]");
            // 对象默认值
            var defaultVal = srcCTLWithJQuery.attr("checkedValue");
            // 判断是否有选择一项
            if (null != defaultVal && defaultVal != undefined) {
                ctls.each(function () {
                    if (this.value == defaultVal) {
                        this.checked = "checked";
                        validator.isValid = true;
                    }
                });
            }
            // 注册单击事件 提示验证提示
            ctls.bind("click", function () {
                var val;
                if (this.validator == undefined) {
                    val = ctls.get(0).validator[0];
                }
                else
                    val = this.validator[0];
                val.isValid = true;
                val.updateShow();
            });
        }

        // 下拉列表
    } else if (srcTag == "SELECT") {
        // 获取当前对象标识
        var groupname = srcCTLWithJQuery.attr("groupname");
        // 获取当前对象
        var ctls = $("select[groupname='" + groupname + "']");

       if (srcCTLWithJQuery.val() != null && srcCTLWithJQuery.val() != "" && srcCTLWithJQuery.val() != "-1" && srcCTLWithJQuery.val() != "0")
            validator.isValid = true;
        // 判断是否有选择一项
        ctls.each(function () {
            var defaultVal = $(this).attr('selectedValue');
            if (null != defaultVal && defaultVal != undefined) {
                $.each(this.options, function () {
                    if ($.trim(this.value) == $.trim(defaultVal) || this.text == defaultVal) {
                        this.selected = true;
                    }
                });
                validator.isValid = true;
            }
        });

        /// 注册控件对象修改值事件
        ctls.bind("change", function () {
            var validators = ctls.get(0).validator;
            for (var i = 0; i < validators.length; i++) {
                if (validators[i].isAjax == null || validators[i].isAjax == undefined) {
                    // 判断验证是否通过
                    validators[i].valid();
                    // 更新显示验证提示 
                    validators[i].updateShow();

                    if (!validators[i].isValid)
                        break;
                }
                else {
                    if (this.id == srcID)
                        validators[i].valid();
                }
            }
        });
    }
}


/// 追加验证条件
function appendValid(validator) {
    var srcCTLWithJQuery = $("#" + validator._parent).get(0);
    if (srcCTLWithJQuery.validator == undefined) {
        srcCTLWithJQuery.validator = new Array();
    }
    if (srcCTLWithJQuery.validator.msgOnEmpty == null) {
        validator.msgOnEmpty = $("#" + validator._parent).attr("alt");
    }
    // 追加的验证在初始化时默认为true 
    validator.isValid = true;
   

    srcCTLWithJQuery.validator.push(validator); 
} 
// 通过同组进行总验证
function PageIsValid(vgroup) { 
    var isValid = true;
    var validateGroup = "default"; // 默认分组
    if (vgroup != "" && vgroup != null) {
        validateGroup = vgroup;
    }
    if (arguments.length > 0)
        validateGroup = arguments[0];

    var ctls = $("[ValidateGroup='" + validateGroup + "']");
    ctls.each(function () {
        if ($("#" + this["id"]).get(0).validator != undefined && $("#" + this["id"]).get(0).validator != null) {
            for (var i = 0; i < $("#" + this["id"]).get(0).validator.length; i++) {
                if ($("#" + this["id"]).get(0).validator[i].isValid == false) {
                    $("#" + this["id"]).get(0).validator[i].updateShow();
                    isValid = false;
                }
            }
        }
    });
    return isValid;
}