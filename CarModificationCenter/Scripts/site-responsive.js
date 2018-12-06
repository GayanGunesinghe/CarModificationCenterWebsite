//count the number of items in the cart and dislay in the badge.

function updateCartBadge() {
    var items = paypalm.minicartk.cart.items(),
        length = items.length,
        count = 0,
        i;

    // Count the number of each item in the cart
    for (i = 0; i < length; i++) {
        count += items[i].get('quantity');
    }
    document.getElementById("cartValue").innerHTML = count; //assign value to badge
}

//update value on documet read and mouseover.
$(document).ready(function() {
    updateCartBadge();
});

$(document).on("mouseover", function()
{
    updateCartBadge();
})