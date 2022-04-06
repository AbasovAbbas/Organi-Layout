$(function () {

    $.ajax({
        type : "Get",
        url: "https://localhost:44346/api/Product",
        success: function(result){
            console.log(result);

            bindProducts(result);
        },
        error: function(result){
            console.log(result);
        }
    })
});

function bindProducts(products){
    $.each(products, function (indexInArray, valueOfElement) {
            let el = $(`<div class="col-4 mt-3">
        <div class="card" style="width: 18rem;">
            <img src="https://picsum.photos/200/300?v=${indexInArray}" class="card-img-top" alt="...">
            <div class="card-body">
              <h5 class="card-title">${valueOfElement.Name}</h5>
              <p class="card-text">${valueOfElement.ShortDescription}</p>
              <a href="#" class="btn btn-primary">Go somewhere</a>
            </div>
          </div>
    </div>`);

            $("#products").append(el);
        });
}