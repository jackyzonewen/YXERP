
/*基础配置*/
seajs.config({
    base: "/modules/",
    alias: {
        "jquery": "/Scripts/jquery-1.11.1.js",
        "global": "scripts/global.js",
        //数据验证
        "verify": "plug/verify.js",
        //HTML模板引擎
        "dot": "plug/doT.js"
    }
});


//seajs.config({
//    alias: {
//        "layout-js": "manage/scripts/manage-layout.js"
//    }
//});

