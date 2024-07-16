function updateCart(productId, quantity) {
    $.ajax({
        url: '@Url.Action("UpdateCartQuantity", "Home")',
        type: 'POST',
        data: { productId: productId, quantity: quantity },
        success: function (result) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log("Error updating cart:", error);
        }
    });
}

function incrementQuantity(productId) {
    var quantityInput = $('#quantity-' + productId);
    var quantity = parseInt(quantityInput.val());
    quantity++;
    quantityInput.val(quantity);
    updateCart(productId, quantity);
}

function decrementQuantity(productId) {
    var quantityInput = $('#quantity-' + productId);
    var quantity = parseInt(quantityInput.val());
    if (quantity > 1) {
        quantity--;
        quantityInput.val(quantity);
        updateCart(productId, quantity);
    }
}