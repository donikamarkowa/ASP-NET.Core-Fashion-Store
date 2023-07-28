function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7245/api/statistics', function (data) {
                $(#total_dresses).text(data.total_dresses + "Dresses");
                $(#total_tshirts).text(data.total_tshirts + "T-Shirts");
                $(#total_trousers).text(data.total_trousers + "Trousers");
                $(#total_jackets).text(data.total_jackets + "Jackets");
                $(#total_jeans).text(data.total_jeans + "Jeans");
                $(#total_accessories).text(data.total_ssessories + "Accessories");
                $(#total_shoes).text(data.total_shoes + "Shoes");

                $('#statistics_box').removeClass('d-none');

                $('#statistics_btn').text('Hide Statistics');
                $('#statistics_btn').removeClass('btn-primary');
                $('#statistics_btn').addClass('btn-danger');
            });
            } else {
                $('#statistics_box').addClass('d-none');

                $('#statistics_btn').text('Show Statistics');
                $('#statistics_btn').removeClass('btn-danger');
                $('#statistics_btn').addClass('btn-primary');
            }     
    });
}