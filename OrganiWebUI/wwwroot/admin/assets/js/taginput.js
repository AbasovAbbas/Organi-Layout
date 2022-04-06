//author: Kamran A-eff

(function ($) {

    $.makeid = function (length) {
        var result = '';
        var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var charactersLength = characters.length;
        for (var i = 0; i < length; i++) {
            result += characters.charAt(Math.floor(Math.random() * charactersLength));
        }
        return result;
    };

    $.fn.tagsinput = function (options) {
        let that = this;

        $.onBound = function (e) {
            let input = $(e.currentTarget);
            let val = $(input).val();

            $(input).prop('size', val.length + 1);

            if (e.type == 'keyup' && e.originalEvent.key != 'Enter') {
                return;
            }

            if (val.length == 0) {
                return;
            }

            if (options.regex != undefined && !options.regex.test(val))
                return;

            let span = $('<span/>', {
                html: val,
                class: 'tag label label-info'
            });

            let current = $('<input/>', { type: 'hidden', name: $(that).attr('name') }).val(val);

            $(span).append(current);

            $(span).append($('<span/>', { 'data-role': 'remove', html: '&times;' })
                .click(function (e) {
                    $(span).remove();
                }));

            $(span).insertBefore(input);
            $(input).val('');
            $(input).prop('size', 1);
        }

        options = $.extend(options, {});

        $(that).each(function (index, element) {

            let input = $('<input/>', { type: 'text', size: 1, class: 'type' })//<input size="1" type="text">

            $(input).on('blur', $.onBound);
            $(input).on('keyup', $.onBound);

            $(element).append(input);

            $.each($(element).data('value').split(';'), function (i, v) {
                $(input).val(v);
                $.onBound({
                    currentTarget: input,
                    type: 'blur'
                });

            });


        });

    };


})(jQuery);