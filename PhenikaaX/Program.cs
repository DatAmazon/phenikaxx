using Microsoft.EntityFrameworkCore;
using PhenikaaX.Entities;
using PhenikaaX.Intefaces;
using PhenikaaX.Interfaces;
using PhenikaaX.IService;
using PhenikaaX.IServices;
using PhenikaaX.MappingProfiles;
using PhenikaaX.Repository;
using PhenikaaX.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PhenikaaXContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PhenikaaXDb")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMedicalBillService, MedicalBillService>();
builder.Services.AddSingleton<IMedicalBillWordDocumentService, MedicalBillWordDocumentService>();
builder.Services.AddScoped<IMedicalBilllWordDocumentBuilder, MedicalBillWordDocumentBuilder>();

builder.Services.AddAutoMapper(typeof(Automapper));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
