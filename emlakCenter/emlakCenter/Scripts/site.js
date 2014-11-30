var url = "localhost:26974";

$(document).ready(function () {

    $("#ilSecimi").change(function () {
        $.get("http://"+url+"/home/getIlce", { id: $(this).val() }, function (data) {
            var ilceler = $.parseJSON(data);
            $("#semtSecimi").html("<option selected>Semt Seçiniz</option>");
            $("#ilceSecimi").html("");
            $("#ilceSecimi").append("<option>İlçe Seçiniz</option>");
            for(var i = 0 ; i<ilceler.length ; i++)
            {
                $("#ilceSecimi").append("<option value='"+ilceler[i].Id+"'>"+ilceler[i].ilce_adi+"</option>");
            }
        });
    });

    $("#ilceSecimi").change(function () {
        $.get("http://"+url+"/home/getSemt", { id: $("#ilceSecimi").val() }, function (data) {
            var ilceler = $.parseJSON(data);
            $("#semtSecimi").html("");
            $("#semtSecimi").append("<option>Semt Seçiniz</option>");
            for (var i = 0 ; i < ilceler.length ; i++) {
                $("#semtSecimi").append("<option value='" + ilceler[i].Id + "'>" + ilceler[i].semt_adi + "</option>");
            }
        });
    });

});