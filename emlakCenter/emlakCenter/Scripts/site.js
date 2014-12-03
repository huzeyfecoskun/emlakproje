//var url = "localhost/emlakCenter";
var url = "localhost:26974"
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

     var skeleton = '<div class="e-ilan">'+
     '   <img src="[[url]]">'+
     '   <div class="e-baslik">'+
     '       [[content]]'+
     '   </div>'+
     '   <hr />'+
     '   <div class="e-aciklama">'+
     '       <div class="e-fiyat">'+
     '           <span class="e-label">Fiyat</span> [[price]]'+
     '       </div>'+
     '       <div class="e-mkare">'+
     '           <span class="e-label">Metrekare</span> [[area]] m<sup>2</sup>'+
     '       </div>'+
     '   </div>'+
     '</div>';

     $.get("http://" + url + "/home/getSearchResults", { queryString: "some-search-credentials" }, function (data) {
         var results = $.parseJSON(data);
         for (var i = 0 ; i < results.length ; i++) {
             //var newSkeleton = skeleton;
             var valuesOld = [
                 "[[url]]",
                 "[[content]]",
                 "[[price]]",
                 "[[area]]"
             ];
             var valuesNew = [
                 results[i].imgUrl,
                 results[i].content,
                 results[i].price,
                 results[i].area
             ];
             $("#e-right").append(replaceWithRespect2Index(valuesOld,valuesNew,skeleton));
         }
     });

});

function replaceWithRespect2Index(oldV, newV, oldText ){
    var newText = oldText;
    if (oldV.length == newV.length) {
        for (var m = 0; m < oldV.length; m++) {
            newText = newText.replace(oldV[m], newV[m]);
        }
    }
    return newText;
}