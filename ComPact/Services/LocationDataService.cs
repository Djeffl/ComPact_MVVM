using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Extensions;
using ComPact.Models;
using ComPact.Repositories;
using ComPact.WebServices;

namespace ComPact.Services
{
	public class LocationDataService : ILocationDataService
	{
		readonly IApiService _apiService;
		readonly ILocationRepository _locationRepository;
		readonly ILocationMemberRepository _locationMemberRepository;
		readonly IRepositoryMapper _mapper;
		readonly IMemberRepository _memberRespository;

		public LocationDataService(IApiService apiService, ILocationRepository locationRepository, IMemberRepository memberRepository, ILocationMemberRepository locationMemberRepository, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_locationRepository = locationRepository;
			_memberRespository = memberRepository;
			_mapper = mapper;
			_locationMemberRepository = locationMemberRepository;
		}

		//public async Task<Payment> Create(Payment payment)
		//{
		//	Payment response = await _apiService.AddPayment(payment);
		//	//TODO Fix dit, je moet het path opslagen die je terug krijgt van je webService
		//	response.Image = payment.Image;
		//	RepoPayment data = _mapper.InvertMap(response);
		//	await _paymentRepository.Insert(data);
		//	return response;
		//}


		public async Task<Location> Create(Location location)
		{
			Location response = await _apiService.AddLocation(location);
			Tuple<RepoLocation, IEnumerable<RepoLocationMember>> data = _mapper.InvertMap(response);
			RepoLocation repoLocationData = data.Item1;
			IEnumerable<RepoLocationMember> repoLocationMember = data.Item2;
			await _locationRepository.Insert(repoLocationData);
			await _locationMemberRepository.Insert(repoLocationMember);

			return response;
		}

		public async Task<bool> Delete(string id)
		{
			bool isSuccessful = await _apiService.DeleteLocation(id);
			if (isSuccessful)
			{
				IEnumerable<RepoLocation> response = await _locationRepository.Where((x => x.Id == id));
				await _locationRepository.Delete(response.FirstOrDefault());
				isSuccessful = true;
			}
			return isSuccessful;
		}

		public async Task<Location> Get(string id, bool isAdmin)
		{
			RepoLocation repoLocation = await _locationRepository?.Get(id);
			IEnumerable<RepoLocationMember> locationMembers = await _locationMemberRepository?.Where(x => x.LocationId == repoLocation.Id);
			Tuple<RepoLocation, IEnumerable<RepoLocationMember>> tuple = new Tuple<RepoLocation, IEnumerable<RepoLocationMember>>(repoLocation, locationMembers);
			Location location = _mapper.Map(tuple);
			if (isAdmin)
			{
				IEnumerable<Member> members = _mapper.Map(await _memberRespository?.All());
				foreach (var member in members)
				{
					for (int memberCount = 0; memberCount < location.Members.Count; memberCount++)
					{
						if (member.Id == location.Members[memberCount].Id)
						{
							location.Members[memberCount] = member;
						}
					}
				}
			}
			return location;
		}

		public async Task<IEnumerable<Location>> GetAll(bool isAdmin)
		{
			IEnumerable<RepoLocation> repoLocations = await _locationRepository?.All();
			IEnumerable<RepoLocationMember> locationMembers = await _locationMemberRepository?.All();
			List<Location> locations = new List<Location>();

			foreach (RepoLocation repoLocation in repoLocations)
			{
				Tuple<RepoLocation, IEnumerable<RepoLocationMember>> tuple = new Tuple<RepoLocation, IEnumerable<RepoLocationMember>>(repoLocation, locationMembers);
				Location location = _mapper.Map(tuple);
				locations.Add(location);
			}
			if (isAdmin)
			{
				IEnumerable<Member> members = _mapper.Map(await _memberRespository?.All());
				foreach (var location in locations)
				{
					foreach (var member in members)
					{
						for (int memberCount = 0; memberCount < location.Members.Count; memberCount++)
						{
							if (member.Id == location.Members[memberCount].Id)
							{
								location.Members[memberCount] = member;
							}
						}
					}
				}
			}
			return locations;
		}

		public async Task<IEnumerable<Location>> GetAll(string userId, bool isAdmin)
		{
			IEnumerable<Location> locations = await _apiService?.GetLocations(userId, isAdmin);
			var tuples = _mapper.InvertMap(locations);
			foreach (var tuple in tuples)
			{
				_locationRepository?.Insert(tuple.Item1);
				_locationMemberRepository?.Insert(tuple.Item2);
			}
			return locations;
		}

		public async Task LogOut()
		{
			await _locationRepository?.Delete(await _locationRepository?.All());
			await _locationMemberRepository?.Delete(await _locationMemberRepository?.All());
			var ls = (await _locationMemberRepository.All()).Convert<RepoLocationMember>();
			System.Diagnostics.Debug.WriteLine(ls.Count);
		}

		public async Task<Location> Update(Location location)
		{
			Location response = await _apiService.UpdateLocation(location);
			Tuple<RepoLocation, IEnumerable<RepoLocationMember>> data = _mapper.InvertMap(response);
			await _locationRepository.Update(data.Item1);
			IEnumerable<RepoLocationMember> locMembers = await _locationMemberRepository.Where((locMem) => locMem.LocationId == data.Item1.Id);
			await _locationMemberRepository.Delete(locMembers);
			await _locationMemberRepository.Insert(data.Item2);
			return response;
		}
	}
}
