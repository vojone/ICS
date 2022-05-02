using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.BL.Models;
using Carpool.DAL.Entities;
using Carpool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Payloads;

//Concrete facade of Ride
namespace Carpool.BL.Facades
{
    public class RideFacade : CRUDFacade<RideEntity, RideListModel, RideDetailModel>
    {
        public RideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {

        }


        public new async Task<RideDetailModel> SaveAsync(RideDetailModel model)
        {
            await using var uow = UnitOfWorkFactory.Create();

            CheckTimeValidity(model);
            await CheckDriverCollisionsAsync(uow, model);
            await CheckParticipantCollisionsAsync(uow, model);

            return await base.SaveAsync(model); 
        }


        private void CheckTimeValidity(RideDetailModel model)
        {
            if (model.ArrivalT < model.DepartureT)
            {
                throw new DbUpdateException("The Arrival Time must be greater than Departure Time!");
            }
        }


        private async Task CheckDriverCollisionsAsync(IUnitOfWork uow, RideDetailModel model)
        {
            //Check collision with other rides for driver and participants
            var driversRideCollisionsQuery = uow
                .GetRepository<RideEntity>()
                .Get()
                .Where(e => e.DriverId == model.DriverId &&
                            e.ArrivalT >= model.DepartureT && //Detecting time collision
                            e.DepartureT <= model.ArrivalT &&
                            e.Id != model.Id); //To prevent fake collisions while updating

            if (await driversRideCollisionsQuery.AnyAsync().ConfigureAwait(false))
            {
                throw new DbUpdateException("There is collision in driver's rides!");
            }
        }


        private async Task CheckParticipantCollisionsAsync(IUnitOfWork uow, RideDetailModel model)
        {
            foreach (var participant in model.Participants)
            {
                if (participant.UserId == model.DriverId)
                {
                    throw new DbUpdateException("Driver cannot be participant at the same time!");
                }

                var participantsRidesCollisionsQuery = uow
                    .GetRepository<RideEntity>()
                    .Get().Include(r => r.Participants)
                    .Where(e => e.Participants.Any(p => p.UserId == participant.UserId) &&
                                e.ArrivalT >= model.DepartureT &&
                                e.DepartureT <= model.ArrivalT &&
                                e.Id != model.Id);

                if (await participantsRidesCollisionsQuery.AnyAsync().ConfigureAwait(false))
                {
                    throw new DbUpdateException("There is collision in participant's rides!");
                }
            }
        }


        public async Task<IEnumerable<RideListModel>> FilterAsync(string? departureLoc = null, 
                                                                  string? arrivalLoc = null, 
                                                                  DateTime? departureTime = null, 
                                                                  DateTime? arrivalTime = null, 
                                                                  bool mustBeAvailable = false)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<RideEntity>()
                .Get()
                .Where(e => 
                    (e.DepartureL == departureLoc || departureLoc == null) &&
                    (e.ArrivalL == arrivalLoc || arrivalLoc == null) &&
                    (e.DepartureT >= departureTime || departureTime == null) &&
                    (e.ArrivalT <= arrivalTime || arrivalTime == null) &&
                    (e.Capacity > 0 || !mustBeAvailable));

            return await Mapper.ProjectTo<RideListModel>(query)
                .ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<RideListModel>> GetByDriverIdAsync(Guid driverId)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<RideEntity>()
                .Get()
                .Where(e => e.DriverId == driverId);

            return await Mapper.ProjectTo<RideListModel>(query)
                .ToArrayAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<RideListModel>> GetByParticipantIdAsync(Guid participantId)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<RideEntity>()
                .Get().Include(i => i.Participants)
                .Where(e => e.Participants
                    .Any(p => p.UserId == participantId));

            return await Mapper.ProjectTo<RideListModel>(query)
                .ToArrayAsync().ConfigureAwait(false);
        }
    }
}
