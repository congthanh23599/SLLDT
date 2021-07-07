

var add= $(".btn_addfile_input");

$(".ctn_addfile").on("click", function () {
    var mb = $(this).attr("data-name");
  
    for (var j = 0, jj = add.length; j < jj;j++  ) {
        if (add[j].dataset.name == mb ){
            $(add[j]).trigger("click");
        }

    }
})