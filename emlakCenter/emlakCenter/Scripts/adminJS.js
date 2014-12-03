/////////////////////////////////////////////////////////////////
// Developer : huzeyfe coşkun
// Developer : halid şenyiğit
// Tarih     : 03.12.2014

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