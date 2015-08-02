
define(function (require, exports, module) {
    var Global = require("global"),
        Verify = require("verify"), VerifyObject,
        doT = require("dot"),
        Easydialog = require("easydialog");
    require("pager");
    require("switch");
    var Params = {
        keyWords: "",
        wareid: "-1",
        pageSize: 20,
        pageIndex: 1,
        totalCount: 0
    }, EntityModel = {
        DepotID: "",
        WareID: ""
    };

    var WareCache;

    var ObjectJS = {};
    //列表页初始化
    ObjectJS.initList = function (wares, wareid) {
        var _self = this;
        WareCache = JSON.parse(wares.replace(/&quot;/g, '"'));
        Params.wareid = wareid;
        EntityModel.WareID = wareid;
        _self.getList();
        _self.bindListEvent();
    }
    //弹出层
    ObjectJS.showCreate = function (wares, callback) {
        var _self = this;
        doT.exec("template/warehouse/depotseat_add.html", function (templateFun) {

            var html = html = templateFun(wares);

            Easydialog.open({
                container: {
                    id: "depotseat-add-div",
                    header: EntityModel.DepotID == "" ? "添加货位" : "编辑货位",
                    content: html,
                    yesFn: function () {

                        if (!VerifyObject.isPass("#depotseat-add-div")) {
                            return false;
                        }
                        var entity = {
                            DepotID: EntityModel.DepotID,
                            Name: $("#name").val().trim(),
                            DepotCode: $("#depotcode").val().trim(),
                            WareID: $("#wareid").val(),
                            Status: $("#status").prop("checked") ? 1 : 0,
                            Description: $("#description").val()
                        };

                        _self.savaEntity(entity, callback);
                    },
                    callback: function () {

                    }
                }
            });

            $("#name").focus();

            //编辑填充数据
            if (EntityModel.DepotID) {
                $("#depotcode").prop("disabled", true);
                $("#wareid").prop("disabled", true);
                $("#name").val(EntityModel.Name);
                $("#depotcode").val(EntityModel.DepotCode);
                $("#wareid").val(EntityModel.WareID);
                $("#status").prop("checked", EntityModel.Status == 1);
                $("#description").val(EntityModel.Description);
               
            }

            VerifyObject = Verify.createVerify({
                element: ".verify",
                emptyAttr: "data-empty",
                verifyType: "data-type",
                regText: "data-text"
            });
        });
    }
    //保存
    ObjectJS.savaEntity = function (entity) {
        var _self = this;
        Global.post("/Warehouse/SaveDepotSeat", { obj: JSON.stringify(entity) }, function (data) {
            if (data.ID.length > 0) {
                _self.getList();
            }
        })
    }

    //绑定列表页事件
    ObjectJS.bindListEvent = function () {
        var _self = this;
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                Params.keyWords = keyWords;
                Params.pageIndex = 1;
                _self.getList();
            });
        });

        $("#sltWareID").val(Params.wareid);

        $("#sltWareID").change(function () {
            Params.wareid = $("#sltWareID").val();
            _self.getList();
        })

        $(".ico-add").on("click", function () {
            EntityModel.DepotID = "";
            _self.showCreate(WareCache);
        });
    }
    //获取列表
    ObjectJS.getList = function () {
        var _self = this;
        $("#warehouse-items").nextAll().remove();
        Global.post("/Warehouse/GetDepotSeats", Params, function (data) {
            doT.exec("template/warehouse/depotseat_list.html", function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $("#warehouse-items").after(innerText);

                //删除事件
                $(".ico-del").click(function () {
                    if (confirm("货位删除后不可恢复,确认删除吗？")) {
                        Global.post("/Warehouse/DeleteDepotSeat", { id: $(this).attr("data-id") }, function (data) {
                            if (data.Status) {
                                _self.getList();
                            } else {
                                alert("删除失败！");
                            }
                        });
                    }
                });
                //编辑事件
                $(".ico-edit").click(function () {
                    Global.post("/Warehouse/GetDepotByID", { id: $(this).attr("data-id") }, function (data) {
                        EntityModel = data.Item;
                        _self.showCreate(WareCache);
                    });
                });
                //绑定启用插件
                innerText.find(".status").switch({
                    open_title: "点击启用",
                    close_title: "点击禁用",
                    value_key: "value",
                    change: function (data,callback) {
                        _self.editStatus(data, data.data("id"), data.data("value"), callback);
                    }
                });
            });
            $("#pager").paginate({
                total_count: data.TotalCount,
                count: data.PageCount,
                start: Params.pageIndex,
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
                    Params.pageIndex = page;
                    Brand.getList();
                }
            });
        });
    }
    //更改状态
    ObjectJS.editStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Warehouse/UpdateDepotSeatStatus", {
            id: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }

    module.exports = ObjectJS;
})