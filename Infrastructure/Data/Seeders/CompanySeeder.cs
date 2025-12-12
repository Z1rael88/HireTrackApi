using Domain.Enums;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public class CompanySeeder(IApplicationDbContext db)
{
    public async Task SeedAsync()
    {
        if (await db.Companies.AnyAsync())
            return;


        var company1 = new Company
        {
            Name = "SoftServe",
            Description =
                "SoftServe to jedna z największych globalnych firm technologicznych wywodzących się z Europy Środkowo-Wschodniej, specjalizująca się w projektowaniu, tworzeniu i wdrażaniu zaawansowanych rozwiązań informatycznych. Firma działa na rynku od ponad 30 lat i zatrudnia tysiące specjalistów na całym świecie, dostarczając usługi dla klientów z USA, Europy i innych regionów.\n\nSoftServe wspiera organizacje w pełnym cyklu rozwoju oprogramowania — od analizy biznesowej i projektowania architektury, poprzez tworzenie aplikacji, implementację chmurową, aż po DevOps, AI/ML i utrzymanie systemów. Firma realizuje projekty dla sektorów takich jak: fintech, zdrowie, e-commerce, edukacja, telekomunikacja oraz nowe technologie.",
            BusinessDomain = BusinessDomain.Fintech,
        };

        await db.Companies.AddAsync(company1);
        await db.SaveChangesAsync();
    }
}