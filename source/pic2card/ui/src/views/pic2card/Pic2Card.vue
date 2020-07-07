<template>
    <div>
        <div class=" d-flex justify-content-end p-1 sticky-top">
            <b-button size="sm" variant="primary" @click="openRenderAll()"
                >Render All</b-button
            >
        </div>
        <div class=" d-flex w-100 ">
            <loading :isLoading="isLoading" :color="'primary'" />
            <div class="flex-wrap  d-flex w-100 justify-content-center">
                <div v-if="isError" class="justify-content-center mt-2 p-2">
                    <b-alert
                        show
                        variant="warning"
                        dismissible
                        @dismissed=";(isError = false), getTemplateImages()"
                    >
                        {{ error }} please try again.!
                    </b-alert>
                </div>
                <div
                    v-else
                    v-for="(item, index) in templates"
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
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Loader from '../../components/loader/Loader.vue'
import CardDetailView from './CardDetailView'
import imageapi from '@/services/ImageApi.js'
import { mapState } from 'vuex'
export default {
    name: 'Pic2card',
    components: {
        loading: Loader
        // CardDetailView: CardDetailView
    },
    data() {
        return {
            isLoading: false,
            base64_images: null,
            isError: false,
            error: ''
        }
    },
    computed: mapState({
        templates: state => state.pic2card.base64_images
    }),
    methods: {
        openDetailView: function(value) {
            this.$router.push({
                name: 'cardDetailView',
                params: {
                    url: value
                }
            })
        },
        openRenderAll: function(value) {
            this.$router.push({
                name: 'renderAllImage'
            })
        },
        getTemplateImages: function() {
            console.log('calling get template image api')
            this.isLoading = true
            imageapi
                .getTemplateImages()
                .then(response => {
                    let cards = response.data['templates']
                    this.$store.dispatch('saveTemplateImages', cards)
                    this.isLoading = false
                })
                .catch(err => {
                    console.log(err)
                    this.isLoading = false
                    this.error = 'Something Went Wrong'
                    this.isError = true
                })
        }
    },

    filters: {
        image_data_url(value) {
            return 'data:image/png;base64,' + value
        }
    },
    created() {
        if (this.templates.length == 0) {
            this.getTemplateImages()
        }
    }
}
</script>
<style lang="scss" scoped>
.p2c-thumbnail {
    cursor: pointer;
}
</style>
