using MotoApp.Components.CsvReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoApp.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Car> ProcessCars(string filePath);
        List<Manufacturer> ProcessManufacturers(string filePath);


    }
}
