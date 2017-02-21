$(document).ready(function () {
    $('#dateDe').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        autoclose: true
    });

    $('#dateAte').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        autoclose: true
    });

    $("#btnPesquisar").click(function () {
        var dataDe = $('#dateDe').val();
        var dataAte = $('#dateAte').val();
    $.ajax({
      type: "POST",
      url: "/Pedido/Index",
      data: {__RequestVerificationToken: $('form[action="/Pedido/Index"] input[name="__RequestVerificationToken"]').val(),  dataDe: dataDe, dataAte: dataAte },
      success: function (response) {
          var result = $('<div />').append(response).find('#divPedidos').html();
          $('#divPedidos').html(result);
          $('#pedidosTable').DataTable({
              language: {
                  "sEmptyTable": "Nenhum registro encontrado",
                  "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                  "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                  "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                  "sInfoPostFix": "",
                  "sInfoThousands": ".",
                  "sLengthMenu": "_MENU_ resultados por página",
                  "sLoadingRecords": "Carregando...",
                  "sProcessing": "Processando...",
                  "sZeroRecords": "Nenhum registro encontrado",
                  "sSearch": "Pesquisar",
                  "oPaginate": {
                      "sNext": "Próximo",
                      "sPrevious": "Anterior",
                      "sFirst": "Primeiro",
                      "sLast": "Último"
                  },
                  "oAria": {
                      "sSortAscending": ": Ordenar colunas de forma ascendente",
                      "sSortDescending": ": Ordenar colunas de forma descendente"
                  }
              }
          });
      }
      //contentType: "application/json; charset=utf-8",
      //dataType: "json",
    });
    });

    
});
GerarRelatorioPedidos = function (e) {

    var model =
        {
            dataDe: $('#dateDe').val(),
            dataAte: $('#dateAte').val()
        }

    $.ajax({
        url: "/Pedido/GerarRelacaoRelatorioPedidos",
        data: model,
        type: 'POST',
        success: function (data) {
            console.log(data);
            $(data).modal({
                backdrop: "static",
                keyboard: true,
                show: true
            }).on('shown.bs.modal', function () {
                $("iframe").css("border", "none");
            }).css({
                width: '1000px',
                height: '650px',
                'margin-left': '250px'
            });
        },
        error: function (result) {
            result.toString();
            console.log("erro");
        }
    });
}