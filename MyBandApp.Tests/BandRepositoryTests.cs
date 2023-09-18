using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyBandApp.Tests;

[TestClass]
public class BandRepositoryTests
{
    [TestMethod]
    public void TestCreateBand()
    {
        // Arrange
        var repository = new BandRepository();
        var band = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };

        // Act
        repository.CreateBand(band);

        // Assert
        var retrievedBand = repository.GetBandById(1);
        Assert.AreEqual("Banda A", retrievedBand.Name);
    }
    
    [TestMethod]
    public void TestCreateBandWithNullBand()
    {
        // Arrange
        var repository = new BandRepository();

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => repository.CreateBand(null));
    }

    [TestMethod]
    public void TestCreateDuplicateBand()
    {
        // Arrange
        var repository = new BandRepository();
        var band = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };

        // Act
        repository.CreateBand(band);

        // Tenta criar a mesma banda novamente
        Action duplicateCreation = () => repository.CreateBand(band);

        // Assert
        Assert.ThrowsException<InvalidOperationException>(duplicateCreation);
    }
   
    [TestMethod]
    public void TestGetBandById()
    {
        // Arrange
        var repository = new BandRepository();
        var band = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };
        repository.CreateBand(band);

        // Act
        var retrievedBand = repository.GetBandById(band.Id);

        // Assert
        Assert.IsNotNull(retrievedBand); // Verifica se a banda foi recuperada com sucesso
        Assert.AreEqual(band.Id, retrievedBand.Id); // Verifica se o ID da banda é o mesmo
    }
    
    [TestMethod]
    public void TestGetAllBands()
    {
        // Arrange
        var repository = new BandRepository();
        var band1 = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };
        var band2 = new Band { Name = "Banda B", Country = "EUA", YearFormed = 1985, MembersCount = 5, Style = "Pop" };
        repository.CreateBand(band1);
        repository.CreateBand(band2);

        // Act
        var bands = repository.GetAllBands();

        // Assert
        Assert.IsNotNull(bands); // Verifica se a lista de bandas não é nula
        Assert.AreEqual(2, bands.Count); // Verifica se há duas bandas na lista
    }

    [TestMethod]
    public void TestUpdateBand()
    {
        // Arrange
        var repository = new BandRepository();
        var band = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };
        repository.CreateBand(band);

        // Act
        var updatedBand = new Band { Name = "Nova Banda A", Country = "Brasil", YearFormed = 1991, MembersCount = 5, Style = "Pop" };
        repository.UpdateBand(1, updatedBand);

        // Assert
        var retrievedBand = repository.GetBandById(1);
        Assert.AreEqual("Nova Banda A", retrievedBand.Name);
        Assert.AreEqual(1991, retrievedBand.YearFormed);
        Assert.AreEqual(5, retrievedBand.MembersCount);
        Assert.AreEqual("Pop", retrievedBand.Style);
    }
    
    [TestMethod]
    public void TestUpdateNonExistentBand()
    {
        // Arrange
        var repository = new BandRepository();
        var band = new Band { Id = 1, Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => repository.UpdateBand(band.Id, band));
    }

    [TestMethod]
    public void TestUpdateBandWithNullName()
    {
        // Arrange
        var repository = new BandRepository();
        var band = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };
        repository.CreateBand(band);

        // Atualize a banda com um nome nulo (ajuste necessário)
        band.Name = null;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => repository.UpdateBand(band.Id, band));
    }

    [TestMethod]
    public void TestUpdateBandWithInvalidId()
    {
        // Arrange
        var repository = new BandRepository();
        var band = new Band { Id = 1, Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };
        repository.CreateBand(band);

        // Atualize a banda com um ID inválido (ajuste necessário)
        var updatedBand = new Band { Id = 2, Name = "Nova Banda A", Country = "EUA", YearFormed = 2000, MembersCount = 5, Style = "Pop" };

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => repository.UpdateBand(updatedBand.Id, updatedBand));
    }

    [TestMethod]
    public void TestDeleteBand()
    {
        // Arrange
        var repository = new BandRepository();
        var band1 = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };
        var band2 = new Band { Name = "Banda B", Country = "EUA", YearFormed = 1985, MembersCount = 5, Style = "Pop" };
        repository.CreateBand(band1);
        repository.CreateBand(band2);

        // Act
        repository.DeleteBand(2); // Deleta a banda com ID 2

        // Assert
        var bands = repository.GetAllBands();
        Assert.AreEqual(1, bands.Count); // Deve haver apenas uma banda após a exclusão
        var remainingBand = bands[0];
        Assert.AreEqual(1, remainingBand.Id); // O ID da banda restante deve ser 1
        Assert.AreEqual("Banda A", remainingBand.Name);
    }
    
    [TestMethod]
    public void TestDeleteNonExistentBand()
    {
        // Arrange
        var repository = new BandRepository();

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => repository.DeleteBand(1)); // Tentativa de excluir uma banda que não existe
    }

    [TestMethod]
    public void TestDeleteBandWithInvalidId()
    {
        // Arrange
        var repository = new BandRepository();
        var band1 = new Band { Name = "Banda A", Country = "Brasil", YearFormed = 1990, MembersCount = 4, Style = "Rock" };
        repository.CreateBand(band1);

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => repository.DeleteBand(0)); // Tentativa de excluir com um ID inválido
    }
}