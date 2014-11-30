$(document).ready(function () {

    $("#ilSecimi").change(function () {
        $.get("http://localhost/emlakCenter/home/getIlce", { id: $(this).val() }, function (data) {
            var ilceler = $.parseJSON(data);
            $("#ilceSecimi").html("");
            $("#ilceSecimi").append("<option>İlçe Seçiniz</option>");
            for(var i = 0 ; i<ilceler.length ; i++)
            {
                $("#ilceSecimi").append("<option value='"+ilceler[i].Id+"'>"+ilceler[i].ilce_adi+"</option>");
            }
        });
    });

    $("#ilceSecimi").change(function () {
        $.get("http://localhost/emlakCenter/home/getSemt", { id: $("#ilceSecimi").val() }, function (data) {
            var ilceler = $.parseJSON(data);
            $("#semtSecimi").html("");
            $("#semtSecimi").append("<option>Semt Seçiniz</option>");
            for (var i = 0 ; i < ilceler.length ; i++) {
                $("#semtSecimi").append("<option value='" + ilceler[i].Id + "'>" + ilceler[i].semt_adi + "</option>");
            }
        });
    });

});