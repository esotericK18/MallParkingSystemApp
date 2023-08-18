using Api.Endpoints;
using DataAccess.DbAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRDBMDataAccess, RDBMDataAccess>();
builder.Services.AddSingleton<IEntryPointRepository, EntryPointRepository>();
builder.Services.AddSingleton<IParkingSlotRepository, ParkingSlotRepository>();
builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
builder.Services.AddSingleton<IPSEPDistanceRepository, PSEPDistanceRepository>();
builder.Services.AddSingleton<IVehicleParkingSlotRepository, VehicleParkingSlotRepository>();
builder.Services.AddSingleton<IParkingSlotService, ParkingSlotService>();
builder.Services.AddSingleton<IParkingService, ParkingService>();
builder.Services.AddSingleton<IFeeComputationService, FeeComputationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//configure endpoints
//app.ConfigureEntryPointApi();
//app.ConfigureParkingSlotApi();
//app.ConfigureParkingSlotEntryPointDistanceApi();
//app.ConfigureVehicleApi();
//app.ConfigureVehicleParkingSlotApi();
app.ConfigureParkingSystemApi();

app.Run();

