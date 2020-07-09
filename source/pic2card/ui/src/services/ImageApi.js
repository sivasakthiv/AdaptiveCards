import axios from 'axios'

const baseURL = 'http://172.17.0.5:5050'

const apiClient = axios.create({
    baseURL: baseURL,
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
    },
    timeout: 2000000
})

export default {
    baseURL() {
        return apiClient.baseURL
    },
    getTemplateImages() {
        return axios.get('json/sample_get_card_templates.json')
        return apiClient.get('/get_card_templates')
    },
    getAdaptiveCard(base64_image) {
        let data = {
            image: base64_image
        }
        let config = {
            header: {
                'Content-Type': 'application/json'
            }
        }
        // Creating fresh client instance to handle the request, as
        // latency of this endpoint is higher.sample_predict_json_debug
        return axios.get('json/sample_predict_json_debug.json')

        return axios({
            method: 'post',
            url: baseURL + '/predict_json_debug',
            timeout: 200000,
            data: { image: base64_image },
            headers: {
                'Content-Type': 'application/json'
            }
        })
    }
}
