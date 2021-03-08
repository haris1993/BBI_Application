var dataTable;
$(document).ready(function () {
    $('select#City').change(function () {
        var name = $(this).children("option:selected").text();
        console.log(name);
        if (name == 'Bratunac' || name == 'Srebrenica') {

            $("#price").on('input', function () {
                var am = $('#price').val();
                var ta = 30;
                var tax = ta / 100;
                var total = am - (am * ta);
                $('#total').html(total);
            });

        //$("#price").on('input', function() {
        //    var numVal1 = $('#price').val();
        //    console.log(numVal1);
        //    var numVal2 = 30 / 100;
        //    var totalValue = numVal1 - (numVal1 * numVal2)
        //    var total = totalValue;
        //    $('#total').html(total);
        //    conso.log(total);
        //});
        alert('Popust za mjesto iz kojeg dolazite');
        }
    });
});



//function priceCalculation() {
//    var numVal1 = $("#price").value();
//    var numVal2 = 30 / 100;
//    var totalValue = numVal1 - (numVal1 * numVal2)
//    var total = totalValue.toFixed(2);
//    $('#total').html(total);
//}

