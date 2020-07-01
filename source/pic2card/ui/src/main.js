import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Loader from './components/loader/Loader.vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

Vue.config.productionTip = false

import VueKonva from 'vue-konva'
Vue.use(VueKonva)

Vue.component('app-loading', Loader)
// Install BootstrapVue
Vue.use(BootstrapVue)
new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')
