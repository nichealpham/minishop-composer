const state = {
  personalInfo: {
    fullName: '',
    genderType: '',
    dob: '',
    avatar: '',
    email: '',
    phone: '',
    identifyId: '',
    address: '',
    occupation: '',
    country: '',
    password: ''
  },
  role: { 
    roleType: null,
  },
  clinicInfo: { 
    clinicName: '',
    shortDescription: '',
    phone: '',
    taxNumber: '',
    address: '',
    logo: ''
  }
}

const getters = {
  getPersonalInfo (state) {
    return state.personalInfo
  },
  getRoleInfo (state) {
    return state.role
  },
  getClinicInfo (state) {
    return state.clinicInfo
  },
  getRegisterInfo (state) {
    return state;
  },
}

const mutations = {
  SET_PROFILE(state, payload) {
    state.personalInfo = {
      fullName: payload.fullName ?? state.personalInfo.fullName,
      genderType: payload.genderType ?? state.personalInfo.genderType,
      dob: payload.dob ?? state.personalInfo.dob,
      avatar: payload.avatar ?? state.personalInfo.avatar,
      email: payload.email ?? state.personalInfo.email,
      phone: payload.phone ?? state.personalInfo.phone,
      identifyId: payload.identifyId ?? state.personalInfo.identifyId,
      address: payload.address ?? state.personalInfo.address,
      occupation: payload.occupation ?? state.personalInfo.occupation,
      country: payload.country ?? state.personalInfo.country,
      password: payload.password ?? state.personalInfo.password,
    }
  },
  SET_ROLE(state, payload) {
    state.role.roleType = payload.userType
  },
  SET_CLINIC(state, payload) {
    state.clinicInfo = {
      clinicName: payload.clinicName ?? state.clinicInfo.clinicName,
      shortDescription: payload.shortDescription ?? state.clinicInfo.shortDescription,
      phone: payload.phone ?? state.clinicInfo.phone,
      taxNumber: payload.taxNumber ?? state.clinicInfo.taxNumber,
      address: payload.address ?? state.clinicInfo.address,
      logo: payload.logo ?? state.clinicInfo.logo,
    }
  }
}

export default {
  namespaced: 'Register',
  state, 
  mutations,
  getters
}