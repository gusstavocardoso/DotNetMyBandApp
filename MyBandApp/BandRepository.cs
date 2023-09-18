using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBandApp;
public class BandRepository
{
    private List<Band> bands;
    private int nextId;

    public BandRepository()
    {
        bands = new List<Band>();
        nextId = 1;
    }

    public void CreateBand(Band band)
    {
        if (bands.Any(b => b.Id == band.Id))
        {
            throw new InvalidOperationException("Já existe uma banda com o mesmo ID.");
        }
        
        if (band == null)
        {
            throw new ArgumentNullException(nameof(band), "O argumento 'band' não pode ser nulo.");
        }

        band.Id = nextId++;
        bands.Add(band);
    }

    public Band GetBandById(int id)
    {
        return bands.FirstOrDefault(b => b.Id == id);
    }

    public List<Band> GetAllBands()
    {
        return bands;
    }

    public void UpdateBand(int id, Band updatedBand)
    {
        if (updatedBand.Name == null)
        {
            throw new ArgumentNullException(nameof(updatedBand.Name), "O nome da banda não pode ser nulo.");
        }
        
        var bandToUpdate = bands.FirstOrDefault(b => b.Id == id);
        if (bandToUpdate != null)
        {
            bandToUpdate.Name = updatedBand.Name;
            bandToUpdate.Country = updatedBand.Country;
            bandToUpdate.YearFormed = updatedBand.YearFormed;
            bandToUpdate.MembersCount = updatedBand.MembersCount;
            bandToUpdate.Style = updatedBand.Style;
        }
        else
        {
            throw new InvalidOperationException("Banda não encontrada.");
        }
    }

    public void DeleteBand(int id)
    {
        var bandToRemove = bands.FirstOrDefault(b => b.Id == id);
        if (bandToRemove != null)
        {
            bands.Remove(bandToRemove);
        }
        else
        {
            throw new InvalidOperationException("Banda não encontrada.");
        }
    }
}
