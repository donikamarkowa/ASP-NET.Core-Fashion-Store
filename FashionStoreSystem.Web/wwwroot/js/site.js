function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7245/api/statistics', function (data) {
                $('#total_dresses').text(data.totalDresses + " pieces");
                $('#total_tshirts').text(data.totalTshirts + " pieces");
                $('#total_trousers').text(data.totalTrousers + " pieces");
                $('#total_jackets').text(data.totalJackets + " pieces");
                $('#total_jeans').text(data.totalJeans + " pieces");
                $('#total_accessories').text(data.totalAccessories + " pieces");
                $('#total_shoes').text(data.totalShoes + " pieces");
                $('#total_swimsuit').text(data.totalSwimsuit + " pieces");

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