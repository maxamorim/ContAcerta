function Pedido() {

    var produtoSelecionado = null;

    Pedido.prototype.BindPedido = function () {
        function ProdutoSelecionado() {
            this.id = 0;
            this.descricao = "";
            this.tipo = "";
            this.valor = 0.00;
        };

        var input = $("#DcPedido");
        input.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Produto/PesquisarProdutos",
                    dataType: "json",
                    type: "POST",
                    data: {
                        descricao: request.term
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            minLength: 2,
            select: function (event, ui) {
                ui.item.value = ui.item.DcProduto;
                produtoSelecionado = new ProdutoSelecionado();
                produtoSelecionado.id = ui.item.ProdutoId;
                produtoSelecionado.descricao = ui.item.DcProduto;
                produtoSelecionado.tipo = ui.item.TpProduto;
                produtoSelecionado.valor = ui.item.ValProduto;
            },
            messages: {
            noResults: '',
            results: function() {}
            }
        }).data("ui-autocomplete")._renderItem = function (ul, item) {
            var $li = $("<li style='cursor:pointer'></li>")
                .attr("data-produto", item.ProdutoId)
                .text(item.DcProduto);

            return $li.appendTo(ul);
        };

    }

    Pedido.prototype.VincularPedido = function (pedidoVinculo) {

        if (produtoSelecionado == null) {
            alert("É necessário selecionar um pedido para vinculá-lo ao pedido");
            return false;
        }

        var $tbody = $("#tabelaPedido");
        var rowCount = $("#tabelaPedido tbody tr[data-produto]").length;
        var id = $("#tabelaPedido tbody tr input[name*='" + pedidoVinculo + "Id']").val();
        var valTotal = parseFloat($('#Valor').val());
        var valArm = parseFloat($('#ValorArmazenado').val());

        BindPedido(produtoSelecionado, id, rowCount, $tbody);

        var calcVal = (valTotal += produtoSelecionado.valor).toString().replace(".", ",");
        var calcArm = (valArm += produtoSelecionado.valor).toString().replace(".", ",");

        $('#Valor').val(calcVal);
        $('.valTot').val(calcVal);
        $('#ValorArmazenado').val(calcArm);
        $('.valTotCob').val(calcArm);
        if ($('#TiposPagamento').val() == 2) {
            $('#ValorDebito').val("");
            $('.valDebito').val("");
            $('#Valor').val(valArm);
            $('.valTot').val(valArm);
            valTotal = parseFloat($('#Valor').val());
            var valDeb = ((2.5 / 100) * valTotal).toFixed(2).toString().replace(".", ",");
            $('#ValorDebito').val(valDeb);
            $('.valDebito').val(valDeb);
            var valDebCalc = ((2.5 / 100) * valTotal);
            var calc = (valTotal - valDebCalc).toString().replace(".", ",");
            console.log(calc);
            $('#Valor').val(calc);
            $('.valTot').val(calc);
        }
        else if ($('#TiposPagamento').val() == 3) {
            $('#ValorCredito').val("");
            $('.valCredito').val("");
            $('#Valor').val(valArm);
            $('.valTot').val(valArm);
            valTotal = parseFloat($('#Valor').val());
            var valCre = ((6 / 100) * valTotal).toFixed(2).toString().replace(".", ",");
            $('#ValorCredito').val(valCre);
            $('.valCredito').val(valCre);
            var valCreCalc = ((6 / 100) * valTotal);
            var calc = (valTotal - valCreCalc).toString().replace(".", ",");
            console.log(calc);
            $('#Valor').val(calc);
            $('.valTot').val(calc);
        }

        $('#DcPedido').val('');

    }

    function BindPedido(produtoSelecionado, id, rowCount, tbody) {
        if (typeof id === "undefined")
            id = 0;

        tbody.find("tr td.td-empty").remove();

        var linha = "<tr data-produto='" + produtoSelecionado.id + "'>"
                        + "<input id='Produtos_" + rowCount + "__ProdutoId' name='Produtos[" + rowCount + "].ProdutoId' type='hidden' value='" + produtoSelecionado.id + "'>"
                        + "<input id='Produtos_" + rowCount + "__DcProduto' name='Produtos[" + rowCount + "].DcProduto' type='hidden' value='" + produtoSelecionado.descricao + "'>"
                        + "<input id='Produtos_" + rowCount + "__TpProduto' name='Produtos[" + rowCount + "].TpProduto' type='hidden' value='" + produtoSelecionado.tipo + "'>"
                        + "<input id='Produtos_" + rowCount + "__ValProduto' name='Produtos[" + rowCount + "].ValProduto' type='hidden' value='" + produtoSelecionado.valor + "'>"
                        + "<td>" + produtoSelecionado.descricao + "</td>"
                        + "<td>" + produtoSelecionado.tipo + "</td>"
                        + "<td>R$" + produtoSelecionado.valor + "</td>"
                        + "<td style='cursor:pointer'><span class='glyphicon glyphicon-trash' onclick='Pedido.prototype.DesvincularPedido(this);' title='Excluir'></span></td>"
                    + "</tr>"
        tbody.append(linha);
        $("#DcPedido").val("").focus();
    }

    Pedido.prototype.DesvincularPedido = function (i) {
        var table = $(i).parents("table");
        $(i).parent().parent().remove();

        var valTotal = parseFloat($('#Valor').val());
        var valArm = parseFloat($('#ValorArmazenado').val());

        var calcVal = (valTotal - produtoSelecionado.valor).toString().replace(".", ",");
        console.log(calcVal);
        var calcArm = (valArm - produtoSelecionado.valor).toString().replace(".", ",");
        console.log(calcArm);

        $('#Valor').val(calcVal);
        $('.valTot').val(calcVal);
        $('#ValorArmazenado').val(calcArm);
        $('.valTotCob').val(calcArm);
        if ($('#TiposPagamento').val() == 2) {
            $('#ValorDebito').val("");
            $('.valDebito').val("");
            $('#Valor').val(calcArm);
            $('.valTot').val(calcArm);
            console.log($('#Valor').val());
            valTotal = parseFloat($('#Valor').val());
            var valDeb = ((2.5 / 100) * valTotal).toFixed(2).toString().replace(".", ",");
            $('#ValorDebito').val(valDeb);
            $('.valDebito').val(valDeb);
            var valDebCalc = ((2.5 / 100) * valTotal);
            var calc = (valTotal - valDebCalc).toString().replace(".", ",");
            console.log(calc);
            $('#Valor').val(calc);
            $('.valTot').val(calc);
        }
        else if ($('#TiposPagamento').val() == 3) {
            $('#ValorCredito').val("");
            $('.valCredito').val("");
            $('#Valor').val(calcArm);
            $('.valTot').val(calcArm);
            valTotal = parseFloat($('#Valor').val());
            var valCre = ((6 / 100) * valTotal).toFixed(2).toString().replace(".", ",");
            $('#ValorCredito').val(valCre);
            $('.valCredito').val(valCre);
            var valCreCalc = ((6 / 100) * valTotal);
            var calc = (valTotal - valCreCalc).toString().replace(".", ",");
            console.log(calc);
            $('#Valor').val(calc);
            $('.valTot').val(calc);
        }

        Pedido.prototype.ReordenarIndices('tabelaPedido');
    }

    Pedido.prototype.ReordenarIndices = function (container) {
        // captura todos os TRs do container
        var trs = $('#' + container + ' tr');
        // Expressão Regular para capturar os índices no formato "[<NUMERAL>]"
        var regularExpressionIndice = new RegExp("\\[\\d+\\]", "i");
        // Expressão Regular para capturar os índices no formato "_<NUMERAL>__"
        var regularExpressionIndice2 = new RegExp("\\_\\d+\\__", "i");
        // Indice a ser utilizado, evitando casos em que o TR não possui campos input do tipo hidden
        var indice = -1;

        $.each(trs, function (keyTr, tr) {
            // Captura todos os campos hidden que possuem indices a serem ordenados no formato "[<NUMERAL>]"
            var inputs = $(tr).find(':input[type="hidden"][name*="["][name*="]"]');

            if (inputs.length > 0) {
                indice++;
            }

            // Realiza o looping de tratamento dos campos
            $.each(inputs, function (keyCampo, campo) {
                // Reordenação de indices de Name (informação recebida pelo Model)
                var novoName = $(campo).attr('name').replace(regularExpressionIndice, '[' + indice + ']');
                $(campo).attr('name', novoName);

                // Reordenação de indices de ID
                var novoId = $(campo).attr('id').replace(regularExpressionIndice, '[' + indice + ']');
                $(campo).attr('id', novoId);

                novoId = $(campo).attr('id').replace(regularExpressionIndice2, '_' + indice + '__');
                $(campo).attr('id', novoId);
            });
        });
    };

    $('#TiposPagamento').change(function () {
        var valTotal = parseFloat($('#Valor').val());
        var valArm = $('#ValorArmazenado').val();
        console.log(valArm);
        var calc = 0;
        if ($('#TiposPagamento').val() == 1) {
            $('#Valor').val(valArm);
            $('.valTot').val(valArm);
            $('#ValorCredito').val("");
            $('.valCredito').val("");
            $('#ValorDebito').val("");
            $('.valDebito').val("");
        }
        else if ($('#TiposPagamento').val() == 2) {
            $('#ValorDebito').val("");
            $('.valDebito').val("");
            $('#ValorCredito').val("");
            $('.valCredito').val("");
            $('#Valor').val(valArm);
            $('.valTot').val(valArm);
            valTotal = parseFloat($('#Valor').val());
            var valDeb = ((2.5 / 100) * valTotal).toFixed(2).toString().replace(".", ",");
            $('#ValorDebito').val(valDeb);
            $('.valDebito').val(valDeb);
            var valDebCalc = ((2.5 / 100) * valTotal);
            calc = (valTotal - valDebCalc).toString().replace(".", ",");
            $('#Valor').val(calc);
            $('.valTot').val(calc);
            
        }
        else if ($('#TiposPagamento').val() == 3) {
            $('#ValorDebito').val("");
            $('.valDebito').val("");
            $('#ValorCredito').val("");
            $('.valCredito').val("");
            $('#Valor').val(valArm);
            $('.valTot').val(valArm);
            valTotal = parseFloat($('#Valor').val());
            var valCre = ((6 / 100) * valTotal).toFixed(2).toString().replace(".", ",");
            $('#ValorCredito').val(valCre);
            $('.valCredito').val(valCre);
            var valCreCalc = ((6 / 100) * valTotal);
            calc = (valTotal - valCreCalc).toString().replace(".", ",");
            $('#Valor').val(calc);
            $('.valTot').val(calc);
        }
    });

    $('#DcPedido').focus(function () {
        $('#notice').fadeOut("slow");
    });

    
}

$(document).ready(function () {
    var pedido = new Pedido();
    pedido.BindPedido();
});

FecharCaixa = function (e) {
    $.ajax({
        url: "/Pedido/FecharCaixa",
        type: 'POST',
        success: function (data) {
            console.log(data);
            alert("Caixa fechado com sucesso. Valor em caixa (dinheiro): R$" + data);
            $('#DcPedido').prop('disabled', true);
            $('#TiposPagamento').prop('disabled', true);
        }
    })
}