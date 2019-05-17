
    $(document).ready(function () {

        CKEDITOR.config.forcePasteAsPlainText = true;
        CKEDITOR.config.toolbar_SimpleCode = [
            ['Cut', 'Copy', 'Paste', 'PasteText'],
            ['Undo', 'Redo'],
            ['Bold', 'Italic', 'Underline']
        ];
        CKEDITOR.config.allowedContent = true;
        CKEDITOR.config.protectedSource.push(/<ins class=\"adsbygoogle\"\>.*?<\/ins\>/g);
        $('.getimagefromelfinder').on('click', function () {
            //get id
            var id = $(this).parent().find('input')[0].id;
            // set id to controller
            window.open("/Portal/File/GetImageFromElfinder?elementId="+ id+'', 'GetImageFromElfinder', 'height=' + (window.screen.height - 100));
        });
    });
