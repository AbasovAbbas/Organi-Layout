(function($){
    $.fn.serializeObject=function(){
        let arr = $(this).serializeArray();
        
        let obj = {};
        $.each(arr, function(indexInArray, valueOfElement){
            if(isNaN(valueOfElement)){
                obj[valueOfElement.name] = valueOfElement.value;
            }else{
                obj[valueOfElement.name] = parseInt(valueOfElement.value);
            }
        });
        return obj;
    };
})(jQuery)
$(function () {

    $('#addproduct').submit(function(e){
        e.preventDefault();

        let obj = $(this).serializeObject();
        let formData = new FormData(e.target);

        console.log(formData.get('file'));
        $.ajax({
            type : "post", 
            url : "https://localhost:44346/api/Image",
            data : formData,
            cache : false,
            contentType : false,
            processData : false,
            success : function(result){
                console.log(result);
                if(result.error == false){
                    //formData.delete("file");
                    obj.ImageToken = result.token;
                    obj.CategoryId = 1;
                    $.ajax({
                        type : "post", 
                        url : "https://localhost:44346/api/Product",
                        data :JSON.stringify(obj),
                        contentType: 'application/json',
                        dataType : "application/json",
                        success : function(result){
                            console.log(result);
                        }
                    });
                }
            },
            error : function(result){
                console.error(result);
            }
        })
    });
});