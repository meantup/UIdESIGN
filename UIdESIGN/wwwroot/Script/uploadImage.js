$(function () {
    $('#formdata').on('submit', function (e) {
        e.preventDefault();
        var formData = $('#formdata').serializeArray();
        formData.push({ "name": "image", "value": reader.result })
        //formData.push('image', reader.result)
        console.log(formData)
        $.ajax({
            type: 'POST',
            enctype: 'multipart/form-data',
            data: formData,
            dataType: 'json',
            url: url.addProduct,
            success: function (response) {
                if (response.isSuccess === true) {
                    Swal.fire({
                        allowOutsideClick: false,
                        title: response.msg + "" + "Inserted!",
                        html: "Product is already Inserted.",
                        icon: 'success'
                    }).then(function () {
                        window.location.reload(); 
                    });
                }
            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    });
});

const proImg = document.getElementById("fileInput");
const prevContainer = document.getElementById("imagePreview");
const previewImage = prevContainer.querySelector(".image-preview__image");
const previewDefaultText = prevContainer.querySelector(".image-preview__default-text");
const reader = new FileReader();
proImg.addEventListener("change", function () {
    const file = this.files[0];
    if (file) {
        previewDefaultText.style.display = "none";
        previewImage.style.display = "block";

        reader.addEventListener("load", function () {
            console.log(this);
            previewImage.setAttribute("src", this.result);
        });
        reader.readAsDataURL(file);
    } else {
        previewDefaultText.style.display = null;
        previewImage.style.display = null;
    }
});


function validateFileType() {
    var fileName = document.getElementById("fileInput").value;
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {
        //TO DO
    } else {
        Swal.fire({
            allowOutsideClick: false,
            title: 'Reminder',
            html: 'Please Select an Image Only.',
            icon: 'warning'
        });
        image = $('#fileInput').val('');
    }
}
