import axios from 'axios';

let axiosInstance;

const getApiClient = () => {
  if (axiosInstance) return axiosInstance;

  axiosInstance = axios.create({
    baseURL: "http://localhost:8732/"
  });

  return axiosInstance;
};

function printTicket(data) {
  return getApiClient()
    .post(
      `PrintService/Print`,
      data,
    )
    .then(response => response.data);
}

export default {
  printTicket
};
