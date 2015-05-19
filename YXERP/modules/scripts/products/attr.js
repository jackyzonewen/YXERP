define(function (require, exports, module) {
    var Global = require("global"),
        Verify = require("verify"), VerifyObject,
        doT = require("dot"),
        Easydialog = require("easydialog");
    require("pager");
    var Attr = {
        AttrID: "",
        AttrName: "",
        Description: ""
    },
    Value = {
        AttrID: "",
        ValueID: "",
        ValueName: ""
    },
    Params = {
        Index: 1,
        KeyWord: ""
    }

    var ObjectJS = {};
    //初始化
    ObjectJS.init = function () {
        var _self = this;
        _self.bindEvent();
        _self.getList();
    }

    //绑定事件
    ObjectJS.bindEvent = function () {
        var _self = this;
        $("#addAttr").click(function () {
            Attr.AttrID = "";
            Attr.AttrName = "";
            Attr.Description = "";
            _self.addAttr();
        });

        //搜索
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                
            });
        });

        $(document).click(function (e) {
            if (!$(e.target).hasClass("attr-values") && !$(e.target).hasClass("attr-value-box") && !$(e.target).parents().hasClass("attr-value-box")) {
                _self.hideValues();
            }
        });
        //添加属性值
        $(".ico-input-add").click(function () {
            if ($("#valueName").val()) {
                Value.ValueID = "";
                Value.ValueName = $("#valueName").val();
                _self.saveValue(function () {
                    $("#valueName").val("");
                });
            }
        })
    }
    ObjectJS.showValues = function (attrID) {
        var height = document.documentElement.clientHeight - 84;
        $("#attrValueBox").css("height", height + "px");
        $("#attrValueBox").animate({ right: "0px" }, "fast");
        Value.AttrID = attrID;
        ObjectJS.getAttrDetail();
    }
    ObjectJS.getAttrDetail = function () {
        Global.post("/Products/GetAttrByID", { attrID: Value.AttrID }, function (data) {
            $("#attrValueBox").find(".header-title").html(data.Item.AttrName);
            ObjectJS.innerValuesItems(data.Item.AttrValues, true);
            
        });
    }

    ObjectJS.hideValues = function () {
        $("#attrValueBox").animate({ right: "-302px" }, "fast");
    }
    //获取属性列表
    ObjectJS.getList = function () {
        var _self = this;
        Global.post("/Products/GetAttrList", { index: Params.Index, keyWorks: Params.KeyWord }, function (data) {
            _self.innerItems(data.Items, true);

            $("#pager").paginate({
                total_count: data.TotalCount,
                count: data.PageCount,
                start: Params.Index,
                display: 5,
                border: true,
                border_color: '#fff',
                text_color: '#333',
                background_color: '#fff',
                border_hover_color: '#ccc',
                text_hover_color: '#000',
                background_hover_color: '#efefef',
                rotate: true,
                images: false,
                mouse: 'slide',
                onChange: function (page) {
                    Params.Index = page;
                    _self.getList();
                }
            });
        });
    }

    //加载属性数据
    ObjectJS.innerItems = function (items, clear) {
        var _self = this;
        if (clear) {
            $("#attrList").empty();
        }
        doT.exec("template/products/attr_list.html", function (templateFun) {
            var inner = templateFun(items);
            inner = $(inner);
            $("#attrList").prepend(inner);
            //点击编辑
            inner.find(".ico-edit").click(function () {
                var _this = $(this), _prev = _this.prev();
                Attr.AttrID = _this.data("id");
                Attr.AttrName = _prev.html();
                Attr.Description = _prev.attr("title");
                _self.addAttr(function () {
                    _prev.html(Attr.AttrName);
                    _prev.attr("title", Attr.Description);
                });
            });

            inner.find(".attr-values").click(function () {
                var _this = $(this);
                _self.showValues(_this.data("id"));
            });
        })
    }

    //加载值数据
    ObjectJS.innerValuesItems = function (items, clear) {
        var _self = this;
        if (clear) {
            $("#attrValues").empty();
        }
        for (var i = 0, j = items.length; i < j; i++) {
            $("#attrValues").prepend('<li data-id="' + items[i].ValueID + '" class="item">' + items[i].ValueName + '</li>');
        }
    }

    //添加子类弹出层
    ObjectJS.addAttr = function (editback) {
        var _self = this;
        var html = '<ul class="create-attr">' +
                        '<li><span class="left">名称：</span><input type="text" id="attrName" value="' + Attr.AttrName + '" class="input verify " data-empty="*必填" /></li>' +
                        '<li><span class="left">描述：</span><textarea id="attrDescription">' + Attr.Description + '</textarea></li>' +
                   '</ul>'
        Easydialog.open({
            container: {
                id: "show-add-attr",
                header: Attr.AttrID == "" ? "添加属性" : "编辑属性",
                content: html,
                yesFn: function () {
                    if (!VerifyObject.isPass()) {
                        return false;
                    }
                    
                    Attr.AttrName = $("#attrName").val();
                    Attr.Description = $("#attrDescription").val();
                    _self.saveAttr(editback);
                },
                callback: function () {
                    
                }
            }
        });
        $("#attrName").focus();
        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
    }

    //保存属性
    ObjectJS.saveAttr = function (editback) {
        var _self = this;
        Global.post("/Products/SaveAttr", { attr: JSON.stringify(Attr) }, function (data) {
            if (data.ID.length > 0) {
                if (!Attr.AttrID) {
                    Attr.AttrID = data.ID;
                    Attr.ValuesStr = "暂无属性值(单击添加)";
                    _self.innerItems([Attr], false);
                }
                !!editback && editback();
            } else {
                alert("操作失败,请稍后重试!");
            }
        });
    }
    //保存属性值
    ObjectJS.saveValue = function (editback) {
        var _self = this;
        Global.post("/Products/SaveAttrValue", { value: JSON.stringify(Value) }, function (data) {
            if (data.ID.length > 0) {
                if (Value.ValueID == "") {
                    Value.ValueID = data.ID;
                    _self.innerValuesItems([Value], false);
                }
                !!editback && editback();
            } else {
                alert("操作失败,请稍后重试!");
            }
        });
    }

    module.exports = ObjectJS;
});