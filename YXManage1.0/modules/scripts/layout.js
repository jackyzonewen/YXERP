
//页面加载
$(document).ready(function () {
    $(".main-body").css("min-height", document.documentElement.clientHeight - 134);
    $(".main-body").css("width", document.documentElement.clientWidth - 218);
    //隐藏下拉菜单
    $(".nav-class").click(
        function () {
            if ($(this).find("img").attr("title") == "展开") {
                $(this).find("img").attr("src", "/modules/images/pull.png").attr("title", "收起");
                $(this).next().slideDown("normal");
            }
            else {
                $(this).find("img").attr("src", "/modules/images/open.png").attr("title", "展开");
                $(this).next().slideUp("normal");
            }
        }
    );
    
});