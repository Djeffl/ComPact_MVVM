using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices;

namespace ComPact
{
	public class MemberDataService: IMemberDataService
	{
		private readonly IApiService _apiService;
		private readonly IMemberRepository _memberRepository;
		readonly IRepositoryMapper _mapper;

		public MemberDataService(IApiService apiService, IMemberRepository memberRepository, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_memberRepository = memberRepository;
			_mapper = mapper;
		}

		public async Task<Member> Get(string memberId)
		{
			return _mapper.Map(await _memberRepository.Get(memberId));
		}
		/**
		 * Web call and save in localstorage
		 */
		public async Task<IEnumerable<Member>> GetAll(string adminId)
		{
			IEnumerable<Member> webResponse = await _apiService.GetMembers(adminId);
			IEnumerable<RepoMember> members = _mapper.InvertMap(webResponse);

			await _memberRepository.Insert(members);

			return await GetAll();
		}
		/**
		 * Local storage call
		 */
		public async Task<IEnumerable<Member>> GetAll()
		{
			return _mapper.Map(await _memberRepository?.All());
		}
		public async Task LogOut()
		{
			await _memberRepository?.Delete(await _memberRepository?.All());
		}
		/**
		* Create User	 
		*/
		public async Task<Member> Create(Member member)
		{
			Member response = await _apiService.AddMember(member);
			RepoMember repoMember = _mapper.InvertMap(response);
			repoMember = await _memberRepository.Insert(repoMember);

			return _mapper.Map(repoMember);
		}
	}
}
	
		