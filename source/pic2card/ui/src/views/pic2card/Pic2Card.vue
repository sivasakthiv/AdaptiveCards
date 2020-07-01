<template>
    <div class=" d-flex w-100 ">
        <loading :isLoading="isLoading" :color="'primary'" />
        <div class="flex-wrap  d-flex w-100">
            <div
                v-for="(item, index) in base64_images"
                :key="index"
                class="col-lg-3 col-md-4 col-6 "
                @click="openDetailView(item)"
            >
                <div class="d-block mb-4 h-100">
                    <b-img-lazy
                        v-bind="{
                            blank: true,
                            blankColor: '#bbb',
                            width: 250,
                            height: 250
                        }"
                        :src="item | image_data_url"
                        rounded
                        class="p2c-thumbnail"
                    ></b-img-lazy>
                </div>
                <!-- <div class=" left-container">
                    <b-img-lazy
                        v-bind="{
                            blank: true,
                            blankColor: '#bbb',
                            width: 400,
                            height: 300
                        }"
                        :src="item | image_data_url"
                        rounded
                    ></b-img-lazy>
                </div> -->
                <!-- <div class="right-container">
                    <cardview :url="item" />
                </div> -->
            </div>
        </div>
    </div>
</template>
<script>
import Loader from '../../components/loader/Loader.vue'
import CardDetailView from './CardDetailView'
import imageapi from '@/services/ImageApi.js'
export default {
    name: 'Pic2card',
    components: {
        loading: Loader
        // CardDetailView: CardDetailView
    },
    data() {
        return {
            isLoading: true,
            base64_images: null
        }
    },
    methods: {
        openDetailView: function(value) {
            this.$router.push({
                name: 'cardDetailView',
                params: {
                    url: value
                }
            })
        }
    },

    filters: {
        image_data_url(value) {
            return 'data:image/png;base64,' + value
        }
    },
    created() {
        this.isLoading = true
        imageapi
            .getTemplateImages()
            .then(response => {
                let cards = response.data['templates']
                this.base64_images = cards
                this.isLoading = false
            })
            .catch(err => {
                console.log(err)
                this.isLoading = falses
            })
    }
}
</script>
<style lang="scss" scoped>
.p2c-thumbnail {
    cursor: pointer;
}
</style>
