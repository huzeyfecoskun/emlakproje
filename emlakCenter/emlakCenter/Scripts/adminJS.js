/////////////////////////////////////////////////////////////////
// Developer : huzeyfe coşkun
// Developer : halid şenyiğit
// Tarih     : 03.12.2014
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