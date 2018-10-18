$(document).ready(function () {

    // Configuraciones Toast
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "200",
        "hideDuration": "1500",
        "timeOut": "6000",
        "extendedTimeOut": "1200",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $("body").on('click', 'button', function () {

 

   

        // Si el boton no tiene el atributo ajax no hacemos nada
        if ($(this).data('ajax') == undefined) return;

        // El metodo .data identifica la entrada y la castea al valor más correcto
        if ($(this).data('ajax') != true) return;

        var form = $(this).closest("form");
        var buttons = $("button", form);
        var button = $(this);
        var url = form.attr('action');

        if (button.data('confirm') != undefined) {
            if (button.data('confirm') == '') {
                if (!confirm('¿Esta seguro de realizar esta acción?')) return false;
            } else {
                if (!confirm(button.data('confirm'))) return false;
            }
        }

        if (button.data('delete') != undefined) {
            if (button.data('delete') == true) {
                url = button.data('url');
            }
        } else {
            if (!form.valid()) {
                return false;
            }
        }

        // Creamos un div que bloqueara todo el formulario
        var block = $('<div class="block-loading" />');
        form.prepend(block);

        // En caso de que haya habido un mensaje de alerta
        $(".alert", form).remove();

        // Para los formularios que tengan CKupdate
        if (form.hasClass('CKupdate')) CKupdate();

        form.ajaxSubmit({
            dataType: 'JSON',
            type: 'POST',
            url: url,
            success: function (r) {
             
                block.remove();
                if (r.response) {
                 
                    if (!button.data('reset') != undefined) {
                        if (button.data('reset')) form.reset();
                    }
                    else {
                        form.find('input:file').val('');
                    }
                }
           
                // Mostrar mensaje
                //if (r.message != null) {
                
                //    if (r.message.length > 0) {
                //        var css = "";
                //        if (r.response) css = "alert-success";
                //        else css = "alert-danger";

                //        var message = '<div class="alert ' + css + ' alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + r.message + '</div>';
                //        form.prepend(message);
                //    }
                //}

                 // Mostrar mensaje Toast
                if (r.message != null) {

                    if (r.message.length > 0) {
                        var css = "";
                        if (r.response) {
                            toastr.success(r.message);
                        }
                        else {
                            toastr.error(r.message);
                        } 

                      
                    }
                }


             
                // Ejecutar funciones
                if (r.function != null) {            
                   
                    setTimeout(r.function, 0);
                }
                // Redireccionar
                if (r.href != null) {
                    console.log('href');
                    if (r.href == 'self') window.location.reload(true);
                    else window.location.href = r.href;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                block.remove();
                toastr.error(errorThrown + ' | ' + textStatus);
                //form.prepend('<div class="alert alert-warning alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + errorThrown + ' | <b>' + textStatus + '</b></div>');
            }
        });

        return false;
    })

    //Plugins();
})

function Plugins() {
    $(".datepicker").datepicker({
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        changeYear: true
    });
}



jQuery.fn.reset = function () {
    $("input:password,input:file,input:text,textarea", $(this)).val('');
    $("input:checkbox:checked", $(this)).click();
    $("select").each(function () {
        $(this).val($("option:first", $(this)).val());
    })
};