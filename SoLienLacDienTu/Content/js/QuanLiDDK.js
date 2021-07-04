

var add= $(".btn_addfile_input");

$(".ctn_addfile").on("click", function () {
    var mb = $(this).attr("data-name");
    var j;
    var jj;
    for (j = 0, jj = add.length; j < jj;j++  ) {
        if (add[j].dataset.name == mb ){
            $(add[j]).trigger("click");
        }

    }
})