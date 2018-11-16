import axios from 'axios';

let axiosInstance;

const getApiClient = () => {
  if (axiosInstance) return axiosInstance;

  axiosInstance = axios.create({
    baseURL: "http://sec31098.ica.ia-hc.net:8088/"
  });

  return axiosInstance;
};

function fetchTicket(id) {
  return getApiClient()
    .get(`api/queue/ticket/${id}`)
    .then(response => response.data);
}

function fetchQueue(queueType) {
  return getApiClient()
    .get(`api/queue/${queueType}`)
    .then(response => response.data);
}

function deleteTicket(id) {
  return getApiClient()
    .delete(`shoppinglists/${id}`);
}

function createTicket(ticket) {
  return getApiClient()
    .post(
      '/api/queue/ticket',
      ticket,
    )
    .then(response => response.data);;
}

export default {
  fetchTicket,
  fetchQueue,
  deleteTicket,
  createTicket,
};
