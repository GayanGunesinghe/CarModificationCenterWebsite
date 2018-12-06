
$(document).ready(function()
{
	"use strict";

	/* 

	Vars and Inits

	*/
    initIsotope();
    initPriceSlider();

	/* 

	Init Isotope

	*/

	function initIsotope()
    {
		var sortingButtons = $('.shop_sorting_button');

		$('.product_grid').isotope({
            itemSelector: '.product_item',
            getSortData: {
            	price: function(itemElement)
            	{
                    var priceEle = $(itemElement).find('.product_price').text().replace( /LKR|,/g, '' );
            		return parseFloat(priceEle);
            	},
            	name: '.product_name div a'
            },
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });

        // Sort based on the value from the sorting_type dropdown
        sortingButtons.each(function()
        {
        	$(this).on('click', function()
        	{
        		$('.sorting_text').text($(this).text());
        		var option = $(this).attr('data-isotope-option');
        		option = JSON.parse(option);
				$('.product_grid').isotope(option);
        	});
        });

	}

	 /* 

      Init Price Slider

	*/

    function initPriceSlider()
    {
    	if($("#slider-range").length)
    	{
    		$("#slider-range").slider(
			{
				range: true,
				min: 100000,
				max: 30000000,
				values: [ 10000000, 20000000 ],
				slide: function( event, ui )
				{
					$( "#amount" ).val( "LKR" + ui.values[ 0 ] + " - LKR" + ui.values[ 1 ] );
				}
			});
				
			$( "#amount" ).val( "LKR " + $( "#slider-range" ).slider( "values", 0 ) + " - LKR " + $( "#slider-range" ).slider( "values", 1 ) );
			$('.ui-slider-handle').on('mouseup', function()
			{
				$('.product_grid').isotope({
		            filter: function()
		            {
		            	var priceRange = $('#amount').val();
			        	var priceMin = parseFloat(priceRange.split('-')[0].replace('LKR', ''));
			        	var priceMax = parseFloat(priceRange.split('-')[1].replace('LKR', ''));
                        var itemPrice = $(this).find('.product_price').clone().children().remove().end().text().replace(/LKR|,/g, '' );

			        	return (itemPrice > priceMin) && (itemPrice < priceMax);
		            },
		            animationOptions: {
		                duration: 750,
		                easing: 'linear',
		                queue: false
		            }
		        });
			});
    	}	
    }
});