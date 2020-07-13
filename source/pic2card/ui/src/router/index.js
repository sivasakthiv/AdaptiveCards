import Vue from 'vue'
import VueRouter from 'vue-router'
import LandingPage from '../views/landing-page/LandingPage.vue'
import Pic2Card from '../views/pic2card/Pic2Card.vue'
import CardDetailView from '../views/pic2card/CardDetailView.vue'
import RenderImage from '../views/render-all-image/RenderImage.vue'
import store from '../store'

Vue.use(VueRouter)

const routes = [
    {
        path: '/cardview',
        name: 'cardDetailView',
        component: CardDetailView,
        props: true
    },
    {
        path: '/renderAll',
        name: 'renderAllImage',
        component: RenderImage,
        props: true
    },
    {
        path: '/Pic2Card',
        name: 'Pic2Card',
        component: Pic2Card
    },
    {
        path: '/',
        name: 'LandingPage',
        component: LandingPage
    }
]

export const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
