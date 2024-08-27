

import axios from 'axios';


const baseurl = 'http://localhost:8080/users';

class UserService {

    userSignIn(authDTO) {
        return axios.post(`${baseurl}/signin`, authDTO);
    }

    userRegistration(userRespDTO) {
        return axios.post(`${baseurl}/userRegistration`, userRespDTO);
    }
    
    createApplication(application) {
        return axios.post(baseurl + 'applications', application, { headers: this.authHeader() });
    }

    updateApplication(applicationId, application) {
        return axios.put(baseurl + `applications/${applicationId}`, application, { headers: this.authHeader() });
    }

    deleteApplication(applicationId) {
        return axios.delete(baseurl + `applications/${applicationId}`, { headers: this.authHeader() });
    }

    getApplicationById(applicationId) {
        return axios.get(baseurl + `applications/${applicationId}`, { headers: this.authHeader() });
    }

    getAllApplications() {
        return axios.get(baseurl + 'applications', { headers: this.authHeader() });
    }

    // authHeader() {
    //     const user = AuthService.getCurrentUser();
    //     if (user && user.token) {
    //         return { Authorization: 'Bearer ' + user.token };
    //     } else {
    //         return {};
    //     }
    // }
}

export default new UserService();
