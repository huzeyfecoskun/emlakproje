/////////////////////////////////////////////////////////////////
// Developer : huzeyfe coşkun
// Developer : halid şenyiğit
// Tarih     : 03.12.2014
var url = "localhost/emlakCenter";
function preview(what) {

    
    var fileTypes = ["jpg", "jpeg", "png"];

    if (what.multiple == true) {

        len = what.files.length;

        for (var j = 0; j < len; j++)
        {
            var source = what.files.item(j).name;
            
            
            var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
            //alert(ext);
            var uzantikont = false;
            for (var i = 0; i < fileTypes.length; i++)
            {
                if (fileTypes[i] == ext)
                {
                    uzantikont = true;
                }
            }

            if (!uzantikont)
           {
                $("#fileInputType").val('');
                alert("BU TÜR DOSYALARI YÜKLEYEMEZSİNİZ!\n\nİzin verilen dosya uzantıları:\n\n" + fileTypes.join(", "));
            }
        }


    }
    
}

$(document).ready(

    function()
    {
        $.get("http://" + url + "/home/getIlce", { id:1 }, function (data) {
            var ilceler = $.parseJSON(data);
            $("#semtSecimi").html("<option selected>Semt Seçiniz</option>");
            $("#ilceSecimi").html("");
            $("#ilceSecimi").append("<option>İlçe Seçiniz</option>");
            for (var i = 0 ; i < ilceler.length ; i++) {
                $("#ilceSecimi").append("<option value='" + ilceler[i].Id + "'>" + ilceler[i].ilce_adi + "</option>");
            }
        });

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
        ImageUpload();
    }

);

function ImageUpload()
{
    $("#galleryID").submit(function () {
        var formdata = new FormData();
        var fileInput = document.getElementById('fileInputType');
        for (i = 0; i < fileInput.files.length; i++) {
            formdata.append(fileInput.files[i].name, fileInput.files[i]);
        }
        var xhr = new XMLHttpRequest();
        if (window.location.href.indexOf('localhost') > -1) {
            xhr.open('POST', 'http://localhost/emlakCenter/admin/UploadImageMethod');
        } else {
            xhr.open('POST', 'http://btstandartlari.org/haberlers/UploadImageMethod');
        }
        xhr.send(formdata);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                if (xhr.responseText == "Success") {
                    alert("Dosya gönderildi");
                }
                else {
                    alert("Gönderilemedi tekrar deneyiniz");
                }
            }
        }
        return false;
    });
}