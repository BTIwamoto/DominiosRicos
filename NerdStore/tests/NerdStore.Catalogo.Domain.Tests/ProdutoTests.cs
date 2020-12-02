using NerdStore.Catalogo.Domain.Valuables;
using System;
using NerdStore.Catalogo.Domain.AggregationObjects;
using NerdStore.Core.DomainObjects.ExceptionHelper;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_Validar_ValidacoesDevemRetornarExceptions()
        {
            var ex = Assert.Throws<DomainException>(() => new Produto(string.Empty, "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1)));
            Assert.Equal("O campo Nome do produto n�o pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() => new Produto("Nome", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1)));
            Assert.Equal("O campo Descri��o do produto n�o pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() => new Produto("Nome", "Descricao", false, 100, Guid.Empty, DateTime.Now, "Imagem", new Dimensoes(1, 1, 1)));
            Assert.Equal("O campo CategoriaId do produto n�o pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() => new Produto("Nome", "Descricao", false, -1, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1)));
            Assert.Equal("O campo Valor do produto n�o pode ser menor que 0", ex.Message);

            ex = Assert.Throws<DomainException>(() => new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(1, 1, 1)));
            Assert.Equal("O campo Imagem do produto n�o pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() => new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(0, 1, 1)));
            Assert.Equal("O campo Altura da dimens�o n�o pode ser menor ou igual a 0", ex.Message);

            ex = Assert.Throws<DomainException>(() => new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(1, 0, 1)));
            Assert.Equal("O campo Largura da dimens�o n�o pode ser menor ou igual a 0", ex.Message);

            ex = Assert.Throws<DomainException>(() => new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(1, 1, 0)));
            Assert.Equal("O campo Profundidade da dimens�o n�o pode ser menor ou igual a 0", ex.Message);
        }
    }
}
