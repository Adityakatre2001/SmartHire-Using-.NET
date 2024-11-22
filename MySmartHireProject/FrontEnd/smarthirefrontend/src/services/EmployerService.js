

import axios from 'axios';
//import AuthService from './AuthService';
import AuthService from './AuthService';

const baseurl = 'http://localhost:8080/employers';
//  const baseurl = 'http://localhost:5249/employers';

class EmployerService {
    createJobPosting(jobPosting) {
        return axios.post(baseurl + 'jobs', jobPosting, { headers: this.authHeader() });
    }

    updateJobPosting(jobId, jobPosting) {
        return axios.put(baseurl + `jobs/${jobId}`, jobPosting, { headers: this.authHeader() });
    }

    deleteJobPosting(jobId) {
        return axios.delete(baseurl + `jobs/${jobId}`, { headers: this.authHeader() });
    }

    getJobPostingById(jobId) {
        return axios.get(baseurl + `jobs/${jobId}`, { headers: this.authHeader() });
    }

    getAllJobPostings() {
        return axios.get(baseurl + 'jobs', { headers: this.authHeader() });
    }

    getApplicationsForJob(jobId) {
        return axios.get(baseurl + `jobs/${jobId}/applications`, { headers: this.authHeader() });
    }

    authHeader() {
        const user = AuthService.getCurrentUser();
        if (user && user.token) {
            return { Authorization: 'Bearer ' + user.token };
        } else {
            return {};
        }
    }
}

export default new EmployerService();
