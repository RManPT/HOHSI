using HOHSI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Data
{
    public static class HOHSIContextExtensions
    {
        public static void EnsureDBSeeded(this HOHSIContext context)
        {
            if (!context.Exercises.Any())
            {
                context.AddRange(new Exercise[]
                {
                    new Exercise()
                    {
                        Name="Fully open",
                        Description="Try to fully open your hand",
                        ImageName = "img\\no-image.jpg"
                    },
                    new Exercise()
                    {
                        Name="Fully close",
                        Description="Try to fully close your hand",
                        ImageName = "img\\no-image.jpg"
                    },
                    new Exercise()
                    {
                        Name="Pinch",
                        Description="Try to fully close your hand",
                        ImageName = "img\\no-image.jpg"
                    },
                    new Exercise()
                    {
                        Name="Auto fully open",
                        Description="Try to fully close your hand",
                        ImageName = "img\\no-image.jpg"
                    },
                    new Exercise()
                    {
                        Name="Auto fully close",
                        Description="Try to fully close your hand",
                        ImageName = "img\\no-image.jpg"
                    }
                });

                context.SaveChanges();
            }

            if (!context.Prescriptions.Any())
            {
                context.AddRange(new Prescription[]
                {
                    new Prescription()
                    {
                        PrescriptionId=1,
                        DateAndTime = DateTime.Now,
                        PatientId = 1,
                        PrescriptorId=1
                    }
                });

                context.SaveChanges();
            }


        }

        public static void EmptyDB(this HOHSIContext context)
        {
            context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            context.Database.Migrate();
            //EnsureDBSeeded(context);
        }
    }
}
