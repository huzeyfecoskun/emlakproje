var url = "localhost/emlakCenter";
//var url = "localhost:26974";
var skeleton = '<div class="e-ilan">' +
     '   <img src="[[url]]">' +
     '   <div class="e-baslik">' +
     '       [[content]]' +
     '   </div>' +
     '   <hr />' +
     '   <div class="e-aciklama">' +
     '       <div class="e-fiyat">' +
     '           <span class="e-label">Fiyat</span> [[price]]' +
     '       </div>' +
     '       <div class="e-mkare">' +
     '           <span class="e-label">Metrekare</span> [[area]] m<sup>2</sup>' +
     '       </div>' +
     '   </div>' +
     '</div>';
$(document).ready(function () {

    $("#ilSecimi").change(function () {
        $.get("http://" + url + "/home/getIlce", { id: $(this).val() }, function (data) {
            var ilceler = $.parseJSON(data);
            $("#semtSecimi").html("<option selected>Semt Seçiniz</option>");
            $("#ilceSecimi").html("");
            $("#ilceSecimi").append("<option>İlçe Seçiniz</option>");
            for (var i = 0 ; i < ilceler.length ; i++) {
                $("#ilceSecimi").append("<option value='" + ilceler[i].Id + "'>" + ilceler[i].ilce_adi + "</option>");
            }
        });
    });

    $("#ilceSecimi").change(function () {
        $.get("http://" + url + "/home/getSemt", { id: $("#ilceSecimi").val() }, function (data) {
            var ilceler = $.parseJSON(data);
            $("#semtSecimi").html("");
            $("#semtSecimi").append("<option>Semt Seçiniz</option>");
            for (var i = 0 ; i < ilceler.length ; i++) {
                $("#semtSecimi").append("<option value='" + ilceler[i].Id + "'>" + ilceler[i].semt_adi + "</option>");
            }
        });
    });

    $("#semtSecimi").change(function () {
        var query = { "semtID": $("#semtSecimi").val() };
        $.post("http://" + url + "/home/getSearchResults", { queryString: JSON.stringify(query) }, function (data) {
            //console.log("I got: " + data);
            var results = $.parseJSON(data);
            $("#e-right").html("");
            for (var i = 0 ; i < results.length ; i++) {
                //var newSkeleton = skeleton;
                $("#e-right").append(replaceWithRespect2Index(results[i], skeleton));
            }
        });
    });

    var end = new Date();
    $('.bas_time').datetimepicker({
        format: 'dd.mm.yyyy',
        weekStart: 1,
        autoclose: 1,
        startView: 2,
        forceParse: 0,
        language: 'tr',
        minView: 2,
        maxView: 2,
        endDate: end,
        pickerPosition: 'top-right'
    });

    $.post("http://" + url + "/home/getSearchResults", { queryString: "first" }, function (data) {
        //console.log("I got: " + data);
        var results = $.parseJSON(data);
        for (var i = 0 ; i < results.length ; i++) {
            //var newSkeleton = skeleton;
            //$("#e-right").append(replaceWithRespect2Index(results[i], skeleton));
        }
    });

});

function replaceWithRespect2Index(obj, oldText) {

    /*var expectedResult = [{
        "Id": 1,
        "il": 60,
        "ilce": 920,
        "semt": 4262,
        "metrekare": 1000000,
        "fiyat": 1000,
        "tapuDurumu": 1,
        "arsaTipi": "İmara açık?",
        "ilgiliBelediye": null,
        "parsel": null,
        "aciklama": "İçerik 1",
        "hasResim": false,
        "hasHarita": false,
        "hasVideo": false
    }];*/

    var oldV = [
                 "[[url]]",
                 "[[content]]",
                 "[[price]]",
                 "[[area]]"
    ];
    var newV = [
        "http://"+url+"/Helper/DosyaAdi/?adres=resim-1.jpg&w=200&h=140",//obj.imgUrl,
        obj.aciklama,
        obj.fiyat,
        obj.metrekare
    ];
    var newText = oldText;
    if (oldV.length == newV.length) {
        for (var m = 0; m < oldV.length; m++) {
            newText = newText.replace(oldV[m], newV[m]);
        }
    }
    return newText;
}

